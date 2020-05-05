using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.WorkOrder;
using Permission = ODMSCommon.CommonValues.PermissionCodes.WorkOrder;
using ODMSModel.Fleet;
using System.Transactions;

using ODMSModel.WorkOrderCard;
using System;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkOrderController : ControllerBase
    {
        #region WorkOrder Index
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderIndex)]
        public ActionResult WorkOrderIndex()
        {
            FillComboBoxes();
            WorkOrderListModel model = new WorkOrderListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            ViewBag.WorkOrderStatusList = new WorkOrderCardBL().ListWorkOrderStats(UserManager.UserInfo).Data;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderIndex)]
        public ActionResult ListWorkOrders([DataSourceRequest]DataSourceRequest request, WorkOrderListModel viewModel)
        {
            var bus = new WorkOrderBL();
            var model = new WorkOrderListModel(request)
            {
                SearchIsActive = viewModel.SearchIsActive,
                DealerId = viewModel.DealerId,
                VehiclePlate = viewModel.VehiclePlate,
                CustomerName = viewModel.CustomerName,
                WorkOrderStatusId = viewModel.WorkOrderStatusId,
                EndDate = viewModel.EndDate,
                StartDate = viewModel.StartDate,
                VehicleType = viewModel.VehicleType,
                VinNo = viewModel.VinNo,
                WorkOrderId = viewModel.WorkOrderId
            };
            var totalCnt = 0;
            var returnValue = bus.ListWorkOrders(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region WorkOrder Create
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderIndex, Permission.WorkOrderCreate)]
        public ActionResult WorkOrderCreate()
        {
            FillComboBoxes();
            WorkOrderViewModel model = new WorkOrderViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderIndex, Permission.WorkOrderCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult WorkOrderCreate(WorkOrderViewModel model)
        {
            FillComboBoxes();
            if (ModelState.IsValid == false)
                return View(model);
            using (var ts = new TransactionScope(TransactionScopeOption.Suppress))
            {
                var bus = new WorkOrderBL();
                model.CommandType = CommonValues.DMLType.Insert;
                bus.DMLWorkOrder(UserManager.UserInfo, model);
                ts.Complete();
            }
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            if (model.ErrorNo > 0) return View(model);
            return RedirectToAction("WorkOrderDetails", new { id = model.WorkOrderId });
            //return View(model);
        }
        #endregion

        #region WorkOrder Update
        //[HttpGet]
        //[AuthorizationFilter(Permission.WorkOrderIndex, Permission.WorkOrderUpdate)]
        //public ActionResult WorkOrderUpdate(int? id)
        //{
        //    FillComboBoxes();
        //    if (!(id.HasValue && id > 0))
        //    {
        //        SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
        //        return View();
        //    }
        //    return View(new WorkOrderBL().GetWorkOrder(id.GetValueOrDefault()));



        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AuthorizationFilter(Permission.WorkOrderIndex, Permission.WorkOrderCreate)]
        //public ActionResult WorkOrderUpdate(WorkOrderViewModel model)
        //{
        //    FillComboBoxes();
        //    if (ModelState.IsValid == false)
        //        return View(model);
        //    var bus = new WorkOrderBL();
        //    model.CommandType = CommonValues.DMLType.Update;
        //    bus.DMLWorkOrder(model);
        //    CheckErrorForMessage(model, true);
        //    ModelState.Clear();
        //    return View(model);
        //}
        #endregion

        #region WorkOrder Delete
        //[HttpPost]
        //[AuthorizationFilter(Permission.WorkOrderIndex, Permission.WorkOrderDelete)]
        //public ActionResult WorkOrderDelete(int? id)
        //{
        //    if (!(id.HasValue && id > 0))
        //    {
        //        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
        //    }
        //    var bus = new WorkOrderBL();
        //    var model = new WorkOrderViewModel { WorkOrderId = id ?? 0 };
        //    model.CommandType = CommonValues.DMLType.Delete;
        //    bus.DMLWorkOrder(model);
        //    CheckErrorForMessage(model, true);
        //    ModelState.Clear();
        //    if (model.ErrorNo == 0)
        //        return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
        //            MessageResource.Global_Display_Success);
        //    return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
        //        model.ErrorMessage);
        //}
        #endregion

        #region WorkOrder Details
        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderIndex, Permission.WorkOrderDetails)]
        public ActionResult WorkOrderDetails(long? id)
        {
            if (!(id.HasValue && id > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            return View(new WorkOrderBL().GetWorkOrderViewModel(UserManager.UserInfo, id.GetValueOrDefault()).Model);
        }
        #endregion

        #region Private Methods

        private void FillComboBoxes()
        {
            var bus = new WorkOrderBL();
            ViewBag.AppointmentTypeList = bus.ListAppointmentTypes(UserManager.UserInfo).Data;
            ViewBag.StuffList = bus.GetDealerUsers(UserManager.UserInfo.GetUserDealerId()).Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            //ViewBag.CountryList = CommonBL.ListCountries();
        }

        #endregion

        public ActionResult GetWorkOrderData(int id, string type)
        {
            switch (type)
            {
                case "Appointment":
                case "Vehicle":
                case "Customer":
                    return Json(new WorkOrderBL().GetWorkOrderData(id, type).Model);
                default:
                    return Json(0);
            }
        }

        public ActionResult GetLastWorkOrderId(int? dealerId)
        {
            var bl = new WorkOrderBL();
            var id = !dealerId.HasValue ? UserManager.UserInfo.GetUserDealerId() : dealerId.Value;
            var result = bl.GetLastWorkOrderId(id).Model;
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetLastWorkOrderPickingId(int? dealerId)
        {
            var bl = new WorkOrderBL();
            var id = !dealerId.HasValue ? UserManager.UserInfo.GetUserDealerId() : dealerId.Value;
            var result = bl.GetLastWorkOrderPickingId(id).Model;
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetLastWorkOrderPickingDetailId(int pickingId)
        {
            var bl = new WorkOrderBL();
            var result = bl.GetLastWorkOrderPickingDetailId(pickingId).Model;
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetLastWorkOrderDetailId(int workOrderId)
        {
            var bl = new WorkOrderBL();
            var result = bl.GetLastWorkOrderDetailId(workOrderId).Model;
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetLastWorkOrderVehicleKM(int vehicleId)
        {
            var bl = new VehicleBL();
            var result = bl.GetLastVehicleKm(vehicleId).Model + 1;
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetWorkOrderInvoiceSerialNoGuid()
        {
            var no = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            return new JsonResult() { Data = no, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult DeleteWorkOrderDetailInvoice(int workOrderDetailId)
        {
            var bl = new WorkOrderInvoicesBL();
            var result = bl.WorkOrderDetailInvoiceDelete(workOrderDetailId).Model;
            return new JsonResult() { Data = result.ErrorMessage, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetLastWorkOrderInvoiceId(int workOrderId)
        {
            var bl = new WorkOrderInvoicesBL();
            var result = bl.GetLastWorkOrderInvoiceId(workOrderId).Model;
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetWorkOrderInvoiceDate()
        {
            return View();
        }

        [AuthorizationFilter(Permission.WorkOrderIndex)]
        public ActionResult GetWorkOrderPartial(long id)
        {
            return PartialView("_WorkOrderDisplay", new WorkOrderBL().GetWorkOrderViewModel(UserManager.UserInfo, id).Model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderIndex)]
        public ActionResult CheckFleet(int customerId, int vehicleId)
        {
            return Json(new { applicableFleetId = new WorkOrderBL().CheckFleet(UserManager.UserInfo, customerId, vehicleId).Model });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderIndex, Permission.WorkOrderCreate)]
        public ActionResult ShowFleetInfo(int id) /*fleetId*/
        {
            if (id > 0)
            {
                var model = new FleetViewModel { IdFleet = id };
                new FleetBL().GetFleet(UserManager.UserInfo, model);
                return PartialView("_FleetInfo", model);
            }
            return new HttpNotFoundResult();
        }

        [HttpPost]
        //  [AuthorizationFilter(Permission.WorkOrderIndex, Permission.WorkOrderUpdate)]
        public JsonResult CancelWorkOrder(WorkOrderCancelModel model)
        {
            if (model.WorkOrderId == 0) return Json(new { Message = MessageResource.Not_Found_Workorder, Result = false });
            if (ModelState.IsValid)
            {
                var bo = new WorkOrderBL().CancelWorkOrder(UserManager.UserInfo, model).Model;
                if (model.ErrorNo == 0)
                {
                    return Json(new { Message = MessageResource.Global_Display_Success, Result = true });
                }
            }
            if (model.CancelReason == null)
            {
                model.ErrorMessage = string.Format(MessageResource.WorkOrderCard_Validation_EnterCancelReason);
            }
            else if (model.CancelReason.Length > 500)
                model.ErrorMessage = string.Format(MessageResource.Validation_Length, 500);

            return Json(new { Message = model.ErrorMessage, Result = false });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WorkOrderIndex)]
        public JsonResult GetVehicleCustomerId(int vehicleId)
        {
            return Json(new WorkOrderBL().GetVehicleCustomerId(vehicleId).Model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.WorkOrderIndex, Permission.WorkOrderCreate)]
        public ActionResult OpenCustomerChange(int customerId, int vehicleCustomerId)
        {
            var model = new WorkOrderBL().GetCustomerChangeData(customerId, vehicleCustomerId).Model;
            return PartialView("_CustomerChangeInfo", model);
        }


        [HttpGet]
        public ActionResult PeriodicMaintHistory(long id)
        {
            ViewBag.id = id;
            return View();
        }
        public ActionResult ListPeriodicMaintHistory(long id)
        {
            var bo = new WorkOrderBL();
            var r = bo.GetPeriodicMaintHistory(id).Data;
            return Json(new
            {
                Data = r
            });
        }

        public ActionResult ShowGuaranteeStatus()
        {
            return View();
        }

    }
}
