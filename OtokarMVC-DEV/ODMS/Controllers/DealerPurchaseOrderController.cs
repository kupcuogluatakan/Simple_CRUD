using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.DealerPurchaseOrder;
using ODMSModel.DealerSaleSparepart;
using ODMSModel.PurchaseOrderDetail;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerPurchaseOrderController : ControllerBase
    {
        //
        // GET: /DealerPurchaseOrder/
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrder.DealerPurchaseOrderIndex)]
        public ActionResult DealerPurchaseOrderIndex(int? id, int? dID, int? sId, int? pId, int? isSuc)
        {
            SetComboBox();
            ViewBag.AllowQuantityEdit = 1;
            if (id == 0 && dID != null && sId != null && pId != null)
            {
                /*
                 * PurchaseOrder yarat ve parametreleri gerekli textboxlara yerleştir.--------              
                 * PartID de parametre geldigi için otomatik "parça ekle" kısmı acık ve ordada parametreler yerleşmiş şeklinde olsun.
                 */
                var bl = new DealerPurchaseOrderBL();
                var model = new DealerPurchaseOrderViewModel
                {
                    PurchaseOrderId = (int)id,
                    DealerId = (int)dID,
                    SupplierId = (int)sId,
                    PartId = (int)pId,
                    PurchaseStatus = CommonValues.PurchaseOrderStatus.NewRecord.GetHashCode(),
                    CommandType = CommonValues.DMLType.Insert,

                    PurchaseOrderType = CommonValues.PurchaseOrderType.Normal.GetHashCode()
                };

                bl.DMLDealerPurchaseOrder(UserManager.UserInfo, model);

                CheckErrorForMessage(model, false);

                return View(model);
            }
            else if (id > 0 && dID == null && sId == null && pId == null)
            {
                /*
                * PurhcaseOrder oncesinde varmış sadece  parametreleri gerekli textboxlara yerleştir.
                * "Parça ekle" kısmı kapalı şekilde açılsın
                */
                var bl = new DealerPurchaseOrderBL();
                var model = new DealerPurchaseOrderViewModel();
                model.PurchaseOrderId = (int)id;

                bl.GetDealerPurchaseOrder(UserManager.UserInfo, model);

                CheckErrorForMessage(model, false);
                if (isSuc != null)
                {
                    if (isSuc > 0)
                    {
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    }

                }

                return View(model);
            }
            else
            {
                var bl = new DealerPurchaseOrderBL();
                var model = new DealerPurchaseOrderViewModel();
                model.PurchaseStatus = CommonValues.PurchaseOrderStatus.NewRecord.GetHashCode();
                model.CommandType = CommonValues.DMLType.Insert;
                model.SupplierId = null;

                if (isSuc != null)
                {
                    if (isSuc > 0)
                    {
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    }

                }

                //bl.DMLDealerPurchaseOrder(model);

                //CheckErrorForMessage(model, true);
                /*
                 *PurchaseOrder Yaratma ekranı acılır
                 *Alttaki tableların vs hiçbiri gözükmez yarattıktan sonra  
                 *http://localhost/ODMS/DealerPurchaseOrder/DealerPurchaseOrderIndex/5 bune yönlendirlir 
                */
                return View(model);
            }

        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrder.DealerPurchaseOrderCreate)]
        public ActionResult DealerPurchaseOrderIndex(DealerPurchaseOrderViewModel model)
        {
            var bl = new DealerPurchaseOrderBL();
            if (model.SupplierId > 0 && model.PurchaseOrderType > 0)
            {
                if (model.CommandType == null)
                {
                    model.CommandType = CommonValues.DMLType.Update;
                }
                else
                {
                    model.CommandType = CommonValues.DMLType.Insert;
                    model.PurchaseStatus = CommonValues.PurchaseOrderStatus.NewRecord.GetHashCode();
                }

                bl.DMLDealerPurchaseOrder(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);

                model.CommandType = null;
                SetComboBox();
                if (model.ErrorNo > 0)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);
                    //return View(model);
                }
                Response.Redirect("~/DealerPurchaseOrder/DealerPurchaseOrderIndex?id=" + model.PurchaseOrderId + "&isSuc=" + (model.ErrorNo == 0 ? "1" : "0"));
            }
            else
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Error_DB_DealerAndPoTypeEmpty;
            }
            CheckErrorForMessage(model, false);
            SetComboBox();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrder.DealerPurchaseOrderPartCreate)]
        public JsonResult AddDealerPurchasePart(int partId, int suppId)
        {
            var bl = new DealerPurchaseOrderBL();
            var model = new DealerPurchaseOrderViewModel { PartId = partId, DealerId = suppId };

            bl.GetPartDetails(UserManager.UserInfo, model);

            return Json(new
            {
                name = model.PartName,
                price = model.ListPrice,
                discountRat = model.DiscountRatio,
                discountPrice = model.DiscountPrice
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrder.DealerPurchaseOrderPartDelete)]
        public JsonResult DeleteDealerPurchasePart(int poDetSeqNo)
        {
            var bl = new DealerPurchaseOrderBL();
            var model = new DealerPurchaseOrderViewModel { PoDetSeqNo = poDetSeqNo };

            bl.DeleteDealerPurhcasePart(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrder.DealerPurchaseOrderComplete)]
        public ActionResult CompleteDealerPurchaseOrder(int poNumber)
        {
            var bl = new DealerPurchaseOrderBL();
            var model = new DealerPurchaseOrderViewModel
            {
                PurchaseOrderId = poNumber
            };

            bl.GetDealerPurchaseOrder(UserManager.UserInfo, model);

            model.OrderDate = DateTime.Now;
            model.PurchaseStatus = CommonValues.PurchaseOrderStatus.OpenPurchaseOrder.GetHashCode();
            model.CommandType = CommonValues.DMLType.Update;

            bl.DMLDealerPurchaseOrder(UserManager.UserInfo, model);

            // TFS NO : 27807 OYA 02.01.2015
            if (model.SupplierId.HasValue)
            {
                DealerBL dBo = new DealerBL();
                DealerViewModel supplierModel = dBo.GetDealer(UserManager.UserInfo, model.SupplierId.GetValue<int>()).Model;
                DealerViewModel dealerModel = dBo.GetDealer(UserManager.UserInfo, model.DealerId).Model;
                string to = supplierModel.ContactEmail;
                string subject = MessageResource.DealerPurchaseOrder_Mail_Subject;
                string body = string.Format(MessageResource.DealerPurchaseOrder_Mail_Body, dealerModel.Name, model.PurchaseOrderId);
                CommonBL.SendDbMail(to, subject, body);
            }

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrder.DealerPurchaseOrderPartCreate)]
        public JsonResult CreateDealerPurchasePart(int partId, int quantity, int poNumber)
        {
            var bl = new DealerPurchaseOrderBL();
            var model = new DealerPurchaseOrderViewModel
            {
                PartId = partId,
                Quantity = quantity,
                PurchaseOrderId = poNumber
            };

            // TFS NO : 29094 aynı parça iki kere eklenemez kontrolü eklendi. OYA 02.03.2015
            int totalCount = 0;
            PurchaseOrderDetailListModel podListModel = new PurchaseOrderDetailListModel();
            podListModel.PurchaseOrderNumber = poNumber;
            PurchaseOrderDetailBL podBo = new PurchaseOrderDetailBL();
            List<PurchaseOrderDetailListModel> detailList = podBo.ListPurchaseOrderDetails(UserManager.UserInfo, podListModel, out totalCount).Data;
            if (totalCount != 0)
            {
                var control = (from e in detailList
                               where e.PartId == partId
                               select e);
                if (control.Any())
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.DealerPurchaseOrderPart_Warning_SamePartExists);
                }
            }

            bl.CreateDealerPurchasePart(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerPurchaseOrder.DealerPurchaseOrderIndex)]
        public ActionResult ListDealerOrderPart([DataSourceRequest] DataSourceRequest request, DealerSaleSparepartListModel dModel)
        {
            var bl = new DealerPurchaseOrderBL();
            var model = new DealerSaleSparepartListModel(request) { PurchaseOrderId = dModel.PurchaseOrderId };
            int totalCount = 0;
            var rValue = bl.ListDealerOrderPart(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        [HttpPost]
        public JsonResult CheckDealerPurchaseOrderPOType(int poType)
        {
            var bl = new DealerPurchaseOrderBL();
            int IsVehicleMust = bl.GetVehicleMustByPOType(poType).Model;

            return Json(new
            {
                IsVehicleMust = IsVehicleMust
            });

        }

        private void SetComboBox()
        {
            ViewBag.SLDealer = CommonBL.ListAllDealerWihSelectListItems().Data;
            ViewBag.SLPOType = PurchaseOrderTypeBL.ListPurchaseOrderTypeAsSelectListItem(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId(), null, null, true, null).Data;
            ViewBag.SLPOStatus = CommonBL.ListLookup(UserManager.UserInfo, "PO_STATUS").Data;

        }
    }
}

