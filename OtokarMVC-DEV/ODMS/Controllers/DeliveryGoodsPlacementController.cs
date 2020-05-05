using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Gma.QrCodeNet.Encoding.DataEncodation;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Reports;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DeliveryGoodsPlacement;
using ODMSModel.DeliveryListPart;

namespace ODMS.Controllers
{
    public class DeliveryGoodsPlacementController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryGoodsPlacement.DeliveryGoodsPlacementIndex)]
        public ActionResult DeliveryGoodsPlacementIndex()
        {
            SetComboBox();
            return View();
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryGoodsPlacement.DeliveryGoodsPlacementIndex)]
        public ActionResult ListDeliveryGoodsPlacement([DataSourceRequest]DataSourceRequest request, DeliveryGoodsPlacementListModel hModel)
        {
            var bl = new DeliveryGoodsPlacementBL();
            var model = new DeliveryGoodsPlacementListModel(request)
            {
                StatusId = hModel.StatusId,
                IsPlaced = hModel.IsPlaced,
                WayBillNo = hModel.WayBillNo,
                WayBillDate = hModel.WayBillDate
            };

            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();

            int totalCount = 0;

            var rValue = bl.ListDeliveryGoodsPlacement(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        private void SetComboBox()
        {
            ViewBag.SLDeliveryStatus = CommonBL.ListLookup(UserManager.UserInfo, "DELIVERY_STATUS").Data;
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryGoodsPlacement.DeliveryGoodsPlacementPartsIndex)]
        public ActionResult PartsPlacementIndex(Int64? deliveryId, int? statusId, bool? isPlaced)
        {
            var model = new DeliveryGoodsPlacementListModel()
            {
                DeliveryId = deliveryId.GetValue<int>(),
                StatusId = statusId,
                IsPlaced = isPlaced.GetValue<bool>()
            };
            ViewBag.AllowQuantityEdit = 1;
            return PartialView(model);
        }

        public ActionResult ListPartsPlacement(PartsPlacementListModel hModel)
        {
            var bl = new DeliveryGoodsPlacementBL();
            var model = new PartsPlacementListModel() { DeliverySeqNo = hModel.DeliverySeqNo };
            int totalCount = 0;

            var rValue = bl.ListPartsPlacement(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }

        [HttpPost]
        public JsonResult ListRackByPart(int id)
        {
            var bl = new DeliveryGoodsPlacementBL();

            return Json(bl.ListRackWarehouseByDetId(UserManager.UserInfo, id).Data);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryGoodsPlacement.DeliveryGoodsPlacementPartsAction)]
        public JsonResult DeleteDeliveryGoodsPlacement(Int64 id)
        {
            var bl = new DeliveryGoodsPlacementBL();
            var model = new PartsPlacementViewModel() { PlacementId = id, CommandType = CommonValues.DMLType.Delete };

            bl.DeletePartsPlacement(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(ControllerBase.AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(ControllerBase.AsynOperationStatus.Error, model.ErrorMessage);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryGoodsPlacement.DeliveryGoodsPlacementPartsAction)]
        public JsonResult SavePartsPlacement()
        {
            var bl = new DeliveryGoodsPlacementBL();
            var resolveRequest = HttpContext.Request;
            var model = new PartsPlacementViewModel();

            var listModel = new List<PartsPlacementListModel>();

            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            {
                var serializer = new JavaScriptSerializer();
                listModel = (List<PartsPlacementListModel>)serializer.Deserialize(jsonString, typeof(List<PartsPlacementListModel>));
            }

            //Check Total Quantity for all sub row and match thir parent request quantity
            var quantityError = listModel.GroupBy(p => p.DeliverySeqNo).Select(c => new PartsPlacementListModel
            {
                Quantity =(c.Sum(p => p.Quantity)),
                Text = c.Select(p => p.Text).FirstOrDefault(),
                DeliverySeqNo = c.Select(p => p.DeliverySeqNo).FirstOrDefault()
            }).Where(p => p.Quantity > Convert.ToDecimal(p.Text));

            if (quantityError.Any())//Quantity not Match
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Error_DB_WorkOrderPickingDetailQuantity;

            }
            else if (listModel.Where(x => x.Quantity == 0).ToList().Count() > 0)
            {      
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.DeliveryGoodsPlacement_Warning_WarehouseRackIsZero;
            }
            else//Quantity Match
            {
                model.ListModel = listModel;
                bl.DMLPartsPlacement(UserManager.UserInfo, model);
            }
            return Json(new
            {
                ErrorNo = model.ErrorNo,
                ErrorMessage = model.ErrorMessage,
                Data = quantityError
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DeliveryGoodsPlacement.DeliveryGoodsPlacementPartsAction)]
        public ActionResult CompleteDeliveryGoodsPlacement(int deliveryId)
        {
            var bl = new DeliveryGoodsPlacementBL();
            var model = new DeliveryListPartSubViewModel { DeliveryId = deliveryId };

            bl.CompleteDeliveryGoodsPlacement(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        public ActionResult PrintPartPlacementForm(int deliveryId)
        {
            byte[] result = null;

            try
            {
                result = ReportManager.GetReport(ReportType.SparePartPositionedReport, deliveryId);
            }
            catch
            {
                return null;
            }

            return File(result, "application/pdf", MessageResource.DeliveryGoodsPlacement_Display_PartPlacementForm);
        }

        [HttpPost]
        public ActionResult ClearDeliveryGoodsPlacement(Int64 deliveryId)
        {
            var bl = new DeliveryGoodsPlacementBL();
            var model = new DeliveryListPartSubViewModel();

            if (deliveryId > 0)
            {
                model.DefaultType = CommonValues.DefaultRackType.Clear;
                model.DeliveryId = deliveryId;
                bl.DMLPartsPlacemntDefault(UserManager.UserInfo, model);
            }

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        public ActionResult DefaultDeliveryGoodsPlacement(Int64 deliveryId)
        {
            var bl = new DeliveryGoodsPlacementBL();
            var model = new DeliveryListPartSubViewModel();

            if (deliveryId > 0)
            {
                model.DefaultType = CommonValues.DefaultRackType.DefaultRack;
                model.DeliveryId = deliveryId;
                bl.DMLPartsPlacemntDefault(UserManager.UserInfo, model);
            }

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        public ActionResult DoReverseTransaction(Int64 deliveryId)
        {
            DeliveryListPartSubViewModel model = new DeliveryListPartSubViewModel() { DeliveryId = deliveryId };

            DeliveryGoodsPlacementBL _deliveryGoodsPlacementService = new DeliveryGoodsPlacementBL();
            _deliveryGoodsPlacementService.DoReverseTransaction(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }


    }
}
