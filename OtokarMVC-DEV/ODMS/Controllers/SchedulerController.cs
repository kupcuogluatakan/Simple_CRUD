using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Schedule;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SchedulerController : ControllerBase
    {
        private SchedulerBL _scheduleService = new SchedulerBL();

        #region Get
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex)]
        public ActionResult ScheduleIndex()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentIndex)]
        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request, string dealerId, string nextToDate, string prevToDate, string calendarToDate)
        {
            var model = new ScheduleViewModel();

            #region Set DealerId
            if (!string.IsNullOrEmpty(dealerId))
            {
                model.DealerId = dealerId.GetValue<int>();
                Session["dealerPostBackId"] = dealerId.GetValue<int>();
            }
            else
            {
                model.DealerId = Session["dealerPostBackId"] != null
                    ? (int)Session["dealerPostBackId"]
                    : ODMSCommon.Security.UserManager.UserInfo.GetUserDealerId();
            }
            #endregion

            #region  Set CalendarToDate
            if (!string.IsNullOrEmpty(calendarToDate))
            {
                model.CurrentWeek = DateTime.Parse(calendarToDate).GetFirstDayOfWeek().DayOfWeek != DayOfWeek.Sunday ?
                                  DateTime.Parse(calendarToDate).GetFirstDayOfWeek() :
                                  DateTime.Parse(calendarToDate);
                Session["CurrentWeek"] = model.CurrentWeek;
            }
            else
            {
                model.CurrentWeek = Session["CurrentWeek"] != null
                   ? (DateTime)Session["CurrentWeek"]
                   : DateTime.Now;
            }
            #endregion

            #region Set NextWeekDate
            if (!string.IsNullOrEmpty(nextToDate))
            {
                model.CurrentWeek = DateTime.Parse(nextToDate).AddDays(1);
                Session["CurrentWeek"] = DateTime.Parse(nextToDate).AddDays(1);
            }
            else
            {
                model.CurrentWeek = Session["CurrentWeek"] != null
                    ? (DateTime)Session["CurrentWeek"]
                    : DateTime.Now;
            }
            #endregion

            #region  Set PrevWeekDate
            if (!string.IsNullOrEmpty(prevToDate))
            {
                model.CurrentWeek = DateTime.Parse(prevToDate).AddDays(-1).GetFirstDayOfWeek();
                Session["CurrentWeek"] = model.CurrentWeek;
            }
            else
            {
                model.CurrentWeek = Session["CurrentWeek"] != null
                   ? (DateTime)Session["CurrentWeek"]
                   : DateTime.Now;
            }
            #endregion

            int totalCnt = 0;

            var workHourList = _scheduleService.WorkHourList(UserManager.UserInfo,model.CurrentWeek, model.DealerId);
            var schedulerList = _scheduleService.List(UserManager.UserInfo,model, out totalCnt).Data;

            schedulerList.AddRange(workHourList);

            return Json(schedulerList.ToDataSourceResult(request));
        }


        #endregion

        #region Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentCreate)]
        public ActionResult ScheduleCreate(string startDate, string endDate)
        {
            ScheduleViewModel model = new ScheduleViewModel();

            FillDropDownListData();

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                startDate = startDate.Replace("GMT 0200 (GTB Standard Time)", string.Empty).Replace("GMT 0300 (GTB Daylight Time)", string.Empty).Replace("GMT 0300 (GTB Standard Time)", string.Empty);
                endDate = endDate.Replace("GMT 0200 (GTB Standard Time)", string.Empty).Replace("GMT 0300 (GTB Daylight Time)", string.Empty).Replace("GMT 0300 (GTB Standard Time)", string.Empty);

                model.AppointmentId = 0;
                model.OptionValue = ((int)SchedulerOptionValue.Appointment).ToString();
                model.DealerId = ODMSCommon.Security.UserManager.UserInfo.GetUserDealerId();
                model.AppointmentDate = startDate.GetValue<DateTime>().ToShortDateString().GetValue<DateTime>();
                model.AppointmentTime = TimeSpan.Parse(startDate.GetValue<DateTime>().ToString("H:mm"));
                model.AppointmentStartDate = startDate.GetValue<DateTime>().ToShortDateString().GetValue<DateTime>();
                model.AppointmentStartTime = TimeSpan.Parse(startDate.GetValue<DateTime>().ToString("H:mm"));
                model.AppointmentEndDate = endDate.GetValue<DateTime>().ToShortDateString().GetValue<DateTime>();
                model.AppointmentEndTime = TimeSpan.Parse(endDate.GetValue<DateTime>().ToString("H:mm"));

                Session["AppointmentDate"] = model.AppointmentDate;
                Session["AppointmentTime"] = model.AppointmentTime;
                Session["AppointmentStartDate"] = model.AppointmentStartDate;
                Session["AppointmentStartTime"] = model.AppointmentStartTime;
                Session["AppointmentEndDate"] = model.AppointmentEndDate;
                Session["AppointmentEndTime"] = model.AppointmentEndTime;
            }

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentCreate)]
        public ActionResult ScheduleCreate(ScheduleViewModel model)
        {
            FillDropDownListData();

            model.OptionValue = Session["ScheduleOptionValue"] != null ? Session["ScheduleOptionValue"].ToString() : string.Empty;
            model.DealerId = Session["ScheduleDealerId"] != null ? (int)Session["ScheduleDealerId"] : 0;
            model.CommandType = CommonValues.DMLType.Insert;

            if (model.IsValid(model))
            {
                if (!_scheduleService.Exists(model))
                    _scheduleService.Insert(UserManager.UserInfo,model);
                else
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.ScheduleViewModel_Display_ErrorMessage;
                }
            }

            if (model.ErrorNo > 0)
            {
                ModelState.Clear();

                model.AppointmentDate = (DateTime)Session["AppointmentDate"];
                model.AppointmentTime = (TimeSpan)Session["AppointmentTime"];
                model.AppointmentStartDate = (DateTime)Session["AppointmentStartDate"];
                model.AppointmentStartTime = (TimeSpan)Session["AppointmentStartTime"];
                model.AppointmentEndDate = (DateTime)Session["AppointmentEndDate"];
                model.AppointmentEndTime = (TimeSpan)Session["AppointmentEndTime"];
            }
            
            if (CheckErrorForMessage(model, true))
                return View(model);
            
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentUpdate)]
        public ActionResult ScheduleUpdate(int? appointmentId, string optionValue, string nonAppId)
        {
            FillDropDownListData();
            var model = new ScheduleViewModel() { AppointmentId = appointmentId, OptionValue = optionValue, NonAppId = nonAppId };

            return View(_scheduleService.Get(UserManager.UserInfo,model).Model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentUpdate)]
        public ActionResult ScheduleUpdate(ScheduleViewModel model)
        {
            FillDropDownListData();

            model.OptionValue = Session["ScheduleOptionValue"] != null ? Session["ScheduleOptionValue"].ToString() : string.Empty;
            model.DealerId = Session["ScheduleDealerId"] != null ? (int)Session["ScheduleDealerId"] : 0;
            model.CommandType = CommonValues.DMLType.Update;
            ViewBag.AppointmentStatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.AppointmentStatusLookup,
                UserManager.LanguageCode).Data;

            if (model.IsValid(model))
            {
                if (!_scheduleService.Exists(model))
                    _scheduleService.Update(UserManager.UserInfo, model);
                else
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.ScheduleViewModel_Display_ErrorMessage;
                }
            }


            if (CheckErrorForMessage(model, true))
                return View(model);

            ModelState.Clear();

            return View(model);
        }

        #endregion

        #region Delete
        [AuthorizationFilter(CommonValues.PermissionCodes.Appointment.AppointmentDelete)]
        public virtual JsonResult Destroy([DataSourceRequest] DataSourceRequest request, int nonAppId)
        {
            var model = new ScheduleViewModel { NonAppId = nonAppId.ToString(CultureInfo.InvariantCulture), CommandType = CommonValues.DMLType.Delete };
            _scheduleService.Delete(UserManager.UserInfo,model);
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion
        [ValidateAntiForgeryToken]
        public void SetParameter(string optionValue, string dealerId)
        {
            Session["ScheduleOptionValue"] = optionValue;
            Session["ScheduleDealerId"] = dealerId.GetValue<int>();
        }

        private void FillDropDownListData()
        {
            var optionList = new List<SelectListItem>();
            var itemApp = new SelectListItem { Text = MessageResource.ScheduleViewModel_Display_AppointmentOpen, Value = ((int)SchedulerOptionValue.Appointment).ToString() };
            var itemUnApp = new SelectListItem { Text = MessageResource.ScheduleViewModel_Display_AppointmentClose, Value = ((int)SchedulerOptionValue.UnAppoinment).ToString() };
            optionList.Add(itemApp);
            optionList.Add(itemUnApp);

            var statusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.AppointmentStatusLookup,
                UserManager.LanguageCode).Data;

            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.AppointmentTypeList = AppointmentTypeBL.ListAppointmentTypeAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.OptionValueList = optionList;
            ViewBag.AppointmentStatusList = statusList;
        }
    }
}
