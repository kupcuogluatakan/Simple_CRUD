using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Appointment;
using ODMSModel.AppointmentIndicatorSubCategory;
using System;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AppointmentController : ControllerBase
    {
        #region Appointment Index
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex)]
        [HttpGet]
        public ActionResult AppointmentIndex()
        {
            FillDropDownListData();
            ViewBag.AppointmentStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.AppointmentStatusLookup,
                UserManager.LanguageCode).Data;
            AppointmentIndexViewModel model = new AppointmentIndexViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            model.StatusId = 1;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex, CommonValues.PermissionCodes.Appointment.AppointmentDetails)]
        public ActionResult ListAppointment([DataSourceRequest]DataSourceRequest request, AppointmentIndexViewModel model)
        {
            var appointmentBo = new AppointmentBL();
            var filter = new AppointmentListModel(request)
            {
                AppointmentTypeId = model.AppointmentTypeId ?? 0,
                CustomerFullName = model.ContactName,
                VehiclePlate = model.VehiclePlate,
                AppointmentDate = model.AppointmentDateStart,
                AppointmentDateEnd = model.AppointmentDateEnd,
                AppointmentStatus = model.StatusId ?? 0
            };

            var totalCnt = 0;
            var response = appointmentBo.ListAppointments(UserManager.UserInfo, filter, out totalCnt);

            return Json(response);
        }
        #endregion

        #region Appointment Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex, CommonValues.PermissionCodes.Appointment.AppointmentCreate)]
        public ActionResult AppointmentCreate()
        {
            FillDropDownListData();
            AppointmentViewModel model = new AppointmentViewModel();
            model.DealerId = UserManager.UserInfo.GetUserDealerId();
            model.Interval = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.AppointmentInterval).Model.GetValue<int>();
            return View(model);

        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex, CommonValues.PermissionCodes.Appointment.AppointmentCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentCreate(AppointmentViewModel model)
        {
            FillDropDownListData();

            if (!ModelState.IsValid)
                return View(model);

            var appointmentBo = new AppointmentBL();
            model.CommandType = CommonValues.DMLType.Insert;
            appointmentBo.DMLAppointment(UserManager.UserInfo, model);

            if (CheckErrorForMessage(model, true))
                return View(model);

            ModelState.Clear();

            return View(new AppointmentViewModel() { DealerId = UserManager.UserInfo.GetUserDealerId(), AppointmentTypeId = null });
        }
        #endregion

        #region Appointment Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex, CommonValues.PermissionCodes.Appointment.AppointmentUpdate)]
        public ActionResult AppointmentUpdate(int? id)
        {
            if (!id.HasValue)
                return View();
            FillDropDownListData();
            return View(GetAppointmentById(id.Value));
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex, CommonValues.PermissionCodes.Appointment.AppointmentUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentUpdate(AppointmentViewModel model)
        {
            FillDropDownListData();
            if (!ModelState.IsValid)
                return View(model);
            var bl = new AppointmentBL();
            model.CommandType = CommonValues.DMLType.Update;
            bl.DMLAppointment(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            return View(model);
        }
        #endregion

        #region Appointment Delete
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex, CommonValues.PermissionCodes.Appointment.AppointmentDelete)]
        public ActionResult AppointmentDelete()
        {
            FillDropDownListData();
            return View();
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex, CommonValues.PermissionCodes.Appointment.AppointmentDelete)]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDelete(AppointmentViewModel model)
        {
            FillDropDownListData();
            if (!ModelState.IsValid)
                return View(model);

            var appointmentBo = new AppointmentBL();
            model.CommandType = CommonValues.DMLType.Insert;
            appointmentBo.DMLAppointment(UserManager.UserInfo, model);

            if (!CheckErrorForMessage(model, true))
            {

            }

            return View(new AppointmentViewModel());

        }
        #endregion

        #region Appointment Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex, CommonValues.PermissionCodes.Appointment.AppointmentDetails)]
        public ActionResult AppointmentDetails(int? id)
        {
            if (!id.HasValue)
                return View();
            FillDropDownListData();
            return View(GetAppointmentById(id.Value));
        }
        #endregion

        #region Appointment Cancel
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex,
            CommonValues.PermissionCodes.Appointment.AppointmentUpdate)]
        public ActionResult AppointmentCancel(int? appointmentId)
        {
            AppointmentBL business = new AppointmentBL();
            var response = business.GetAppointment(UserManager.UserInfo, appointmentId.GetValue<int>());
            response.Model.StatusId = (int)CommonValues.AppointmentStatus.Cancelled;
            response.Model.CommandType = CommonValues.DMLType.Update;
            business.DMLAppointment(UserManager.UserInfo, response.Model);
            if (response.Model.ErrorNo > 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, response.Model.ErrorMessage);

            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }

        #endregion

        [HttpGet]
        public JsonResult ListVehicleTypes(string vehicleModelCode)
        {
            return !string.IsNullOrWhiteSpace(vehicleModelCode) ? Json(LabourDurationBL.GetVehicleTypeList(UserManager.UserInfo, vehicleModelCode, null).Data, JsonRequestBehavior.AllowGet) : Json(null, JsonRequestBehavior.AllowGet);
        }

        private AppointmentViewModel GetAppointmentById(int id)
        {
            var business = new AppointmentBL();
            var response = business.GetAppointment(UserManager.UserInfo, id);
            CheckErrorForMessage(response.Model, false);
            return response.Model;
        }

        private void FillDropDownListData()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.AppointmentTypeList = AppointmentTypeBL.ListAppointmentTypeAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAppointmentCustomer(int customerId, int appointmentId)
        {
            var business = new AppointmentBL();
            var filter = new AppointmentCustomerViewModel { CustomerId = customerId, AppointmentId = appointmentId };
            var response = business.GetAppointmentCustomer(UserManager.UserInfo, filter);
            return Json(response.Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAppointmentVehicleInfo(int vehicleId, int appointmentId)
        {
            var business = new AppointmentBL();
            var response = business.GetAppointmentVehicleInfo(vehicleId, appointmentId);
            return Json(new
            {
                Type = response.Model.VehicleType,
                Model = response.Model.VehicleModel,
                VehicleColor = response.Model.VehicleColor,
                Vin = response.Model.VehicleVin,
                Plate = response.Model.VehiclePlate,
                CustomerId = response.Model.CustomerId
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetAppointmentIndicatorSubCategoryInfo(int subCategoryId)
        {
            AppointmentIndicatorSubCategoryViewModel aiscModel = new AppointmentIndicatorSubCategoryViewModel();
            aiscModel.AppointmentIndicatorSubCategoryId = subCategoryId;
            AppointmentIndicatorSubCategoryBL aiscBo = new AppointmentIndicatorSubCategoryBL();
            aiscBo.GetAppointmentIndicatorSubCategory(UserManager.UserInfo, aiscModel);

            return Json(new
            {
                MainCategoryId = aiscModel.AppointmentIndicatorMainCategoryId,
                CategoryId = aiscModel.AppointmentIndicatorCategoryId,
                SubCategoryId = aiscModel.AppointmentIndicatorSubCategoryId
            });
        }

        [HttpPost]
        public JsonResult GetAppointmentData(int id, string type, int? appointmentId)
        {
            return Json(new AppointmentBL().GetAppointmentData(id, type, appointmentId));
        }
        [HttpPost]
        public JsonResult GetAppointmentPeriod(int dealerId, string appointmentDate, string appointmentTimeHours,
           string appointmentTimeMinutes, string appointmentTimeSeconds)
        {
            DateTime appDate = appointmentDate.GetValue<DateTime>();
            TimeSpan appTime = new TimeSpan(appointmentTimeHours.GetValue<int>(), appointmentTimeMinutes.GetValue<int>(), appointmentTimeSeconds.GetValue<int>());
            return Json(new
            {
                appointmentInterval = 
                new AppointmentBL().GetAppointmentPeriod(dealerId, appDate, appTime)
            });
        }
    }
}
