using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Delivery;
using ODMSModel.SupplierDispatchPart;
using System.Collections.Generic;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class SupplierDispatchPartController : ControllerBase
    {
        private readonly SupplierDispatchPartBL _supplierDispatchPartService = new SupplierDispatchPartBL();

        [AuthorizationFilter(CommonValues.PermissionCodes.SupplierDispatchPart.SupplierDispatchPartIndex)]
        public ActionResult SupplierDispatchPartIndex(int id, int suppId, string wayBillNo, string wayBillDate, int statusId)
        {
            return PartialView(new SupplierDispatchPartListModel()
            {
                DeliveryId = id,
                SupplierId = suppId,
                WayBillNo = wayBillNo,
                WayBillDate = wayBillDate,
                StatusId = statusId
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SupplierDispatchPart.SupplierDispatchPartList)]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request, SupplierDispatchPartListModel model)
        {
            int totalCnt;

            var referenceModel = new SupplierDispatchPartListModel(request) { DeliveryId = model.DeliveryId, StatusId = model.StatusId };

            var retValue = _supplierDispatchPartService.List(UserManager.UserInfo,referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = retValue,
                Total = totalCnt
            });
        }

        #region Create

        [AuthorizationFilter(CommonValues.PermissionCodes.SupplierDispatchPart.SupplierDispatchPartCreate)]
        public ActionResult SupplierDispatchPartCreate(int id)
        {
            return PartialView(new SupplierDispatchPartViewModel()
            {
                DeliveryId = id
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SupplierDispatchPart.SupplierDispatchPartCreate)]
        public ActionResult SupplierDispatchPartOrderCreate(int id, int suppId, string wayBillNo, string wayBillDate)
        {
            return PartialView(new SupplierDispatchPartOrderViewModel()
            {
                SupplierId = suppId,
                DeliveryId = id,
                WayBillNo = wayBillNo,
                WayBillDate = wayBillDate,
            });
        }

        [HttpPost]
        public ActionResult SupplierDispatchPartCreate(SupplierDispatchPartViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                _supplierDispatchPartService.Insert(UserManager.UserInfo,model);

                foreach (var item in ModelState.Keys.Where(item => item != "DeliveryId"))
                    ModelState[item].Value = new ValueProviderResult(string.Empty, string.Empty, CultureInfo.CurrentCulture);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }


        [HttpPost]
        public ActionResult SupplierDispatchPartOrderCreate(SupplierDispatchPartOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                _supplierDispatchPartService.Insert(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }
        
        #endregion

        #region Update

        [AuthorizationFilter(CommonValues.PermissionCodes.SupplierDispatchPart.SupplierDispatchPartUpdate)]
        public ActionResult SupplierDispatchPartUpdate(int id)
        {
            return PartialView(_supplierDispatchPartService.Get(UserManager.UserInfo, new SupplierDispatchPartViewModel() { DeliverySeqNo = id }).Model);
        }

        [HttpPost]
        public ActionResult SupplierDispatchPartUpdate(SupplierDispatchPartViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                _supplierDispatchPartService.Update(UserManager.UserInfo,model);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SupplierDispatchPart.SupplierDispatchPartUpdate)]
        [HttpPost]
        public ActionResult SupplierDispatchPartSave([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<SupplierDispatchPartListModel> supplierDispatchPartListModel)
        {
            var retValue = _supplierDispatchPartService.Update(UserManager.UserInfo,supplierDispatchPartListModel).Model;

            return retValue.ErrorNo == 0 ?
               GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
               GenerateAsyncOperationResponse(AsynOperationStatus.Error, retValue.ErrorMessage);
        }

        #endregion

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.SupplierDispatchPart.SupplierDispatchPartDelete)]
        public ActionResult SupplierDispatchPartDelete(int id, int statusId)
        {
            var model = new SupplierDispatchPartViewModel() { DeliverySeqNo = id, CommandType = CommonValues.DMLType.Delete, StatusId = statusId };

            if (model.StatusId == -1)
            {
                _supplierDispatchPartService.Delete(UserManager.UserInfo,model);
            }
            else
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Delivery_Message_CantDeleteDelivery;
            }


            return model.ErrorNo == 0 ?
                GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success) :
                GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        public ActionResult CompleteSupplierDispatchPart(Int64 id)//deliveryMstId
        {
            var bl = new SupplierDispatchPartBL();
            var model = new DeliveryViewModel()
            {
                DeliveryId = id,
                StatuId = (int) CommonValues.DeliveryStatus.ReceivedCompletely
            };
            DeliveryBL deliveryBo = new DeliveryBL();
            DeliveryViewModel deliveryModel = deliveryBo.GetDelivery(UserManager.UserInfo,id).Model;
            if (deliveryModel.StatuId == -1)
            {
                bl.ChangeDeliveryMstStatus(UserManager.UserInfo,model);

                return model.ErrorNo == 0
                           ? GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                            MessageResource.Global_Display_Success)
                           : GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.SupplierDispatchPart_Warning_Complete);
            }
        }
    }
}
