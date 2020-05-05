using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.AppointmentDetails;
using ODMSModel.AppointmentDetailsLabours;
using Permission = ODMSCommon.CommonValues.PermissionCodes.AppointmentDetailsLabours;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AppointmentDetailsLaboursController : ControllerBase
    {
        #region Index
        [AuthorizationFilter(Permission.AppointmentDetailsLaboursIndex)]
        public ActionResult AppointmentDetailsLaboursIndex(int id = 0)
        {
            var bl = new AppointmentDetailsLaboursBL();
            var model = new AppointmentDetailsLaboursViewModel
            {
                HideElements = id < 1,
                AppointmentIndicatorId = id

            };
            bl.GetAppIndicType(model);
            return PartialView(model);
        }
        [AuthorizationFilter(Permission.AppointmentDetailsLaboursIndex)]
        public ActionResult ListAppointmentDetailsLabours([DataSourceRequest]DataSourceRequest request, int AppointmentIndicatorId = 0)
        {
            var bus = new AppointmentDetailsLaboursBL();
            var model = new AppointmentDetailsLaboursListModel(request)
            {
                AppointmentIndicatorId = AppointmentIndicatorId
            };
            var totalCnt = 0;
            var list = AppointmentIndicatorId > 0
                ? bus.ListAppointmentIndicatorLabours(UserManager.UserInfo, model, out totalCnt).Data
                : new List<AppointmentDetailsLaboursListModel>();
            return Json(new { Data = list, Total = totalCnt });
        }

        #endregion

        #region Create
        [AuthorizationFilter(Permission.AppointmentDetailsLaboursIndex, Permission.AppointmentDetailsLaboursCreate)]
        public ActionResult AppointmentDetailsLaboursCreate(int id = 0)
        {
            ViewBag.HideElements = id <= 0;
            var indicator = new AppointmentDetailsLaboursBL().GetIndicatorData(UserManager.UserInfo, id).Model;
            ViewBag.Indicator = indicator;
            ViewBag.HideElements = indicator.AppointmentId <= 0;
            return View(new AppointmentDetailsLaboursViewModel { AppointmentIndicatorId = id, AppointmentId = indicator.AppointmentId });
        }
        [HttpPost]
        [AuthorizationFilter(Permission.AppointmentDetailsLaboursIndex, Permission.AppointmentDetailsLaboursCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDetailsLaboursCreate(AppointmentDetailsLaboursViewModel model)
        {
            ViewBag.HideElements = model.AppointmentIndicatorId <= 0;
            var indicator = model.AppointmentIndicatorId > 0 ? new AppointmentDetailsLaboursBL().GetIndicatorData(UserManager.UserInfo, model.AppointmentIndicatorId).Model : new AppointmentDetailsViewModel();
            ViewBag.Indicator = indicator;
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new AppointmentDetailsLaboursBL();
            model.CommandType = CommonValues.DMLType.Insert;
            bus.DMLAppointmentDetailLabours(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(new AppointmentDetailsLaboursViewModel { AppointmentIndicatorId = model.AppointmentIndicatorId, AppointmentId = model.AppointmentId });
        }
        #endregion

        #region Update
        [AuthorizationFilter(Permission.AppointmentDetailsLaboursIndex, Permission.AppointmentDetailsLaboursUpdate)]
        public ActionResult AppointmentDetailsLaboursUpdate(int id = 0)
        {
            var model = new AppointmentDetailsLaboursBL().GetAppointmentDetailsLabour(UserManager.UserInfo, id).Model;
            var indicator = id > 0 ? new AppointmentDetailsLaboursBL().GetIndicatorData(UserManager.UserInfo, model.AppointmentIndicatorId).Model : new AppointmentDetailsViewModel();
            ViewBag.Indicator = indicator;
            ViewBag.HideElements = id <= 0;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.AppointmentDetailsLaboursUpdate, Permission.AppointmentDetailsLaboursIndex)]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDetailsLaboursUpdate(AppointmentDetailsLaboursViewModel model)
        {
            ViewBag.HideElements = model.AppointmentIndicatorId <= 0;
            var indicator = model.AppointmentIndicatorId > 0 ? new AppointmentDetailsLaboursBL().GetIndicatorData(UserManager.UserInfo, model.AppointmentIndicatorId).Model : new AppointmentDetailsViewModel();
            ViewBag.Indicator = indicator;
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new AppointmentDetailsLaboursBL();
            model.CommandType = CommonValues.DMLType.Update;
            bus.DMLAppointmentDetailLabours(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(model);
        }
        #endregion

        #region Delete
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.AppointmentDetailsLaboursIndex, Permission.AppointmentDetailsLaboursDelete)]
        public ActionResult AppointmentDetailsLaboursDelete(int? id)
        {
            if (!(id.HasValue && id > 0))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }
            var bus = new AppointmentDetailsLaboursBL();
            var model = new AppointmentDetailsLaboursViewModel
            {
                AppointmentIndicatorLabourId = id ?? 0,
                CommandType = CommonValues.DMLType.Delete
            };
            bus.DMLAppointmentDetailLabours(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);
        }
        #endregion
    }
}
