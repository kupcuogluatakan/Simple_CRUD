using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Kendo.Mvc.UI;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Delivery;
using ODMSModel.DeliveryGoodsPlacement;
using ODMSModel.DeliveryListPart;
using ODMSModel.PurchaseOrder;
using System.Linq;
using ODMSModel.PurchaseOrderDetail;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class DeliveryListPartController : ControllerBase
    {
        //
        // GET: /DeliveryListPart/

        public ActionResult DeliveryListPartIndex(int deliveryId, int statusId)
        {
            var model = new DeliveryListPartListModel() { DeliveryId = deliveryId, Status = statusId };
            ViewBag.AllowQuantityEdit = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.AllowStockPickQuantityEdit).Model;
            return PartialView(model);
        }

        public ActionResult ListDeliveryListPart([DataSourceRequest]DataSourceRequest request, DeliveryListPartListModel hModel)
        {
            var bl = new DeliveryListPartBL();
            var model = new DeliveryListPartListModel(request) { DeliveryId = hModel.DeliveryId };
            int totalCount = 0;

            List<DeliveryListPartListModel> rValue = bl.ListDeliveryListPart(UserManager.UserInfo, model, out totalCount).Data;

            // TFS NO : 28335 OYA 03.02.2015 kabul adedinin düşmesi için eklendi.
            foreach (DeliveryListPartListModel dpListModel in rValue)
            {
                var detailBo = new DeliveryGoodsPlacementBL();
                var detailModel = new PartsPlacementListModel() { DeliverySeqNo = dpListModel.DeliverySeqNo };
                int detailtotalCount = 0;

                var detailList = detailBo.ListPartsPlacement(UserManager.UserInfo, detailModel, out detailtotalCount).Data;
                if (totalCount != 0)
                {
                    var total = (from e in detailList.AsEnumerable()
                                 select e.Quantity).Sum();
                    dpListModel.RemainingQnty = dpListModel.ReceiveQnty - total;
                }
            }


            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        [HttpPost]
        public JsonResult SaveDeliveryListPart()
        {
            var bl = new DeliveryListPartBL();
            var model = new DeliveryListPartViewModel();

            var resolveRequest = HttpContext.Request;
            var listModel = new List<DeliveryListPartListModel>();

            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            {
                var serializer = new JavaScriptSerializer();
                listModel = (List<DeliveryListPartListModel>)serializer.Deserialize(jsonString, typeof(List<DeliveryListPartListModel>));
            }

            model.ListModel = listModel;
            bl.DMLDeliveryListPart(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteDeliveryListPart(Int64 deliveryId)
        {
            var bl = new DeliveryListPartBL();
            var model = new DeliveryListPartViewModel() { DeliveryId = deliveryId };

            /*TFS NO : 28466 OYA 10.02.2015
             Stok giriş ekranında tamamla denildiğinde ilgili kaydın statü kontrolü yapılmalı. Eğer statü teslim alınmadı veya teslime başlandı 
             * değil ise işleme devam edilmemeli. 
             * Eğer tamamla denilen kayıt statüsü yeni kayıt ise İrsaliye girişi tamamlanmadığı için işleme devam edilemiyor. Uyarısı verilmeli.
             * Eğer statü eksik teslim alındı veya teslim alındı ise Stok giriş yapıldığı için işleme devam edilemiyor. uyarısı verilmeli. */
            DeliveryBL deliveryBo = new DeliveryBL();
            DeliveryViewModel deliveryModel = deliveryBo.GetDelivery(UserManager.UserInfo, deliveryId).Model;

            if (deliveryModel.StatuId == (int)CommonValues.DeliveryStatus.NewRecord)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                      MessageResource.DeliveryList_Warning_NewRecord);
            }
            if (deliveryModel.StatuId == (int)CommonValues.DeliveryStatus.ReceivedPartially ||
                   deliveryModel.StatuId == (int)CommonValues.DeliveryStatus.ReceivedCompletely)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                      MessageResource.DeliveryList_Warning_Received);
            }
            if (deliveryModel.StatuId == (int)CommonValues.DeliveryStatus.NotReceived
                || deliveryModel.StatuId == (int)CommonValues.DeliveryStatus.StartToReceive)
            {
                //------------------------------------------------------------------------------------------------------------------------------//
                /*TFS NO : 27953 nolu isteğe istinaden yapıldı OYA 12.01.2015*/
                List<int> poNumberList = new List<int>();
                int totalCount = 0;
                PurchaseOrderBL poBo = new PurchaseOrderBL();
                PurchaseOrderDetailBL poDetBo = new PurchaseOrderDetailBL();

                DeliveryListPartListModel dlplModel = new DeliveryListPartListModel();
                dlplModel.DeliveryId = deliveryId;

                DeliveryListPartBL dlpBo = new DeliveryListPartBL();
                List<DeliveryListPartListModel> detailList = dlpBo.ListDeliveryListPart(UserManager.UserInfo, dlplModel, out totalCount).Data as List<DeliveryListPartListModel>;

                foreach (DeliveryListPartListModel detailModel in detailList)
                {
                    PurchaseOrderDetailViewModel poDetModel = new PurchaseOrderDetailViewModel();
                    poDetModel.PurchaseOrderDetailSeqNo = detailModel.PoDetSeqNo;
                    poDetBo.GetPurchaseOrderDetail(UserManager.UserInfo, poDetModel);

                    if (poDetModel.ShipmentQuantity >= poDetModel.OrderQuantity &&
                        poDetModel.StatusId == (int)CommonValues.PurchaseOrderDetailStatus.Open)
                    {
                        poDetModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Closed;
                    }
                    poDetModel.CommandType = CommonValues.DMLType.Update;
                    poDetBo.DMLPurchaseOrderDetail(UserManager.UserInfo, poDetModel);
                    if (poDetModel.ErrorNo > 0)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, poDetModel.ErrorMessage);
                    }
                    if (!poNumberList.Contains(poDetModel.PurchaseOrderNumber))
                    {
                        poNumberList.Add(poDetModel.PurchaseOrderNumber);
                    }
                }

                foreach (int poNumber in poNumberList)
                {
                    PurchaseOrderDetailListModel listModel = new PurchaseOrderDetailListModel();
                    listModel.PurchaseOrderNumber = poNumber;
                    listModel.StatusId = (int)CommonValues.PurchaseOrderDetailStatus.Open;
                    poDetBo.ListPurchaseOrderDetails(UserManager.UserInfo, listModel, out totalCount);
                    if (totalCount == 0)
                    {
                        PurchaseOrderViewModel poModel = new PurchaseOrderViewModel();
                        poModel.PoNumber = poNumber;
                        poBo.GetPurchaseOrder(UserManager.UserInfo, poModel);
                        poModel.Status = (int)CommonValues.PurchaseOrderStatus.ClosePurchaseOrder;
                        poModel.CommandType = CommonValues.DMLType.Update;
                        poBo.DMLPurchaseOrder(UserManager.UserInfo, poModel);
                        if (poModel.ErrorNo > 0)
                        {
                            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, poModel.ErrorMessage);
                        }
                    }
                }
                //------------------------------------------------------------------------------------------------------------------------------//

                bl.CompleteDeliveryListPart(UserManager.UserInfo, model);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.DeliveryList_Warning_CompleteStatus);
            }

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

    }
}
