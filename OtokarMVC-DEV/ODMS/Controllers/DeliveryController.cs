using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Gma.QrCodeNet.Encoding.DataEncodation;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Delivery;
using ODMSModel.SupplierDispatchPart;
using Permission = ODMSCommon.CommonValues.PermissionCodes.Delivery;
namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DeliveryController : ControllerBase
    {
        #region Get actions
        [HttpGet]
        [AuthorizationFilter(Permission.DeliveryIndex)]
        public ActionResult DeliveryIndex()
        {
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, "DELIVERY_STATUS").Data;
            ViewBag.SupplierList = new CommonBL().ListSuppliersByDealerId(UserManager.UserInfo.GetUserDealerId()).Data;
            return View();
        }
        [HttpGet]
        [AuthorizationFilter(Permission.DeliveryIndex, Permission.DeliveryCreate)]
        public ActionResult DeliveryCreate()
        {
            ViewBag.SupplierList = new CommonBL().ListSuppliersByDealerId(UserManager.UserInfo.GetUserDealerId()).Data;
            return View();
        }

        [HttpGet]
        [AuthorizationFilter(Permission.DeliveryIndex, Permission.DeliveryUpdate)]
        public ActionResult DeliveryUpdate(long id)
        {
            ViewBag.SupplierList = new CommonBL().ListSuppliersByDealerId(UserManager.UserInfo.GetUserDealerId()).Data;
            var returnModel = new DeliveryBL().GetDelivery(UserManager.UserInfo, id).Model;
            var model = new DeliveryCreateModel()
            {
                DeliveryId = returnModel.DeliveryId,
                StatusId = returnModel.StatuId,
                SupplierId = returnModel.SupplierId,
                WayBillDate = returnModel.WaybillDate,
                WayBillNo = returnModel.WaybillNo,
                InvoiceDate = returnModel.InvoiceDate,
                InvoiceNo = returnModel.InvoiceNo,
                InvoiceSerialNo = returnModel.InvoiceSerialNo
            };
            return View(model);
        }
        [HttpGet]
        [AuthorizationFilter(Permission.DeliveryIndex, Permission.DeliveryDetails)]
        public ActionResult DeliveryDetails(int id)
        {
            var returnModel = new DeliveryBL().GetDelivery(UserManager.UserInfo, id).Model;

            return View(returnModel);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.DeliveryIndex, Permission.DeliveryDetails)]
        public ActionResult PurchaseSelect(int id, bool partSearch = false)
        {
            ViewBag.SupplierId = id;
            ViewBag.PartSearch = partSearch;
            return View("_PurchaseOrdersSearch");
        }

        [AuthorizationFilter(Permission.DeliveryIndex)]
        public JsonResult ListDelivery([DataSourceRequest]DataSourceRequest request, DeliveryListModel viewModel)
        {
            var bus = new DeliveryBL();
            var model = new DeliveryListModel(request);
            model.Status = viewModel.Status;
            model.WayBillNo = viewModel.WayBillNo;
            model.WayBillDate = viewModel.WayBillDate;
            model.SupplierId = viewModel.SupplierId;
            var totalCnt = 0;
            var returnValue = bus.ListDelivery(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }



        #endregion

        //#region Post actions

        [HttpPost]
        [AuthorizationFilter(Permission.DeliveryIndex, Permission.DeliveryCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryCreate(DeliveryCreateModel model)
        {
            ViewBag.SupplierList = new CommonBL().ListSuppliersByDealerId(UserManager.UserInfo.GetUserDealerId()).Data;
            var bus = new DeliveryBL();

            if (bus.Exists(model.SupplierId, model.WayBillNo, UserManager.UserInfo.GetUserDealerId()).Model)
            {
                SetMessage(MessageResource.Delivery_Warning_SupplierWaybill, CommonValues.MessageSeverity.Fail);
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.CommandType = CommonValues.DMLType.Insert;
            bus.DMLDelivery(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo > 0)
            {
                return View(model);
            }

            return RedirectToAction("DeliveryUpdate", new { id = model.DeliveryId });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.DeliveryIndex, Permission.DeliveryDelete)]
        public ActionResult DeliveryDelete(int id)
        {
            var bus = new DeliveryBL();
            var model = new DeliveryCreateModel { DeliveryId = id, CommandType = CommonValues.DMLType.Delete };
            bus.DMLDelivery(UserManager.UserInfo, model);
            ViewBag.HideFormElements = false;

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.DeliveryIndex, Permission.DeliveryUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryUpdate(DeliveryCreateModel model)
        {
            var bus = new DeliveryBL();
            ViewBag.SupplierList = new CommonBL().ListSuppliersByDealerId(UserManager.UserInfo.GetUserDealerId()).Data;
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (bus.Exists(model.SupplierId, model.WayBillNo, UserManager.UserInfo.GetUserDealerId()).Model)
            {
                SetMessage(MessageResource.Delivery_Warning_SupplierWaybill, CommonValues.MessageSeverity.Fail);
                return View(model);
            }

            model.CommandType = CommonValues.DMLType.Update;
            bus.DMLDelivery(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);

            if (model.ErrorNo == 0)
                SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
            else
                SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);

            return RedirectToAction("DeliveryUpdate", new { id = model.DeliveryId });

        }
        [HttpPost]
        public ActionResult SupplierDispatchPartExternalCreate(SupplierDispatchPartViewModel model)
        {
            var bl = new SupplierDispatchPartBL();
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                bl.Insert(UserManager.UserInfo, model);

                foreach (var item in ModelState.Keys.Where(item => item != "DeliveryId"))
                    ModelState[item].Value = new ValueProviderResult(string.Empty, string.Empty, CultureInfo.CurrentCulture);

                CheckErrorForMessage(model, true);
            }

            return RedirectToAction("DeliveryUpdate", new { id = model.DeliveryId });
        }
        [HttpPost]
        [AuthorizationFilter(Permission.DeliveryIndex, Permission.DeliveryUpdate)]
        public ActionResult Cancel(long id = 0)
        {
            if (id == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Err_Generic_Unexpected);

            var model = new DeliveryBL().CancelDelivery(UserManager.UserInfo, deliveryId: id).Model;
            if (model.ErrorNo == 1)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, model.ErrorMessage);
        }

        [HttpPost]
        public ActionResult CheckDeleteItem(DeliveryCreateModel model)
        {
            return Json(new
            {
                new DeliveryBL().CheckDeleteItem(UserManager.UserInfo, model).Model.HasDeleteItem,
                model.DeliveryId
            });
        }

        [HttpPost]
        public ActionResult DeleteDeliveryItem(int deliveryId)
        {
            var model = new DeliveryCreateModel() { DeliveryId = Convert.ToInt64(deliveryId) };

            new DeliveryBL().DeleteDeliveryItem(model);

            return Json(new
            {
                Status = true
            });
        }
    }
}
