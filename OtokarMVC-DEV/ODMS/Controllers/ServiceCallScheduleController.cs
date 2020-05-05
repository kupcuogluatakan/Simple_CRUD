using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSBusiness;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ServiceCallScheduleController : ControllerBase
    {
        private readonly ServiceCallScheduleBL _serviceCallScheduleBl = new ServiceCallScheduleBL();

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.ServiceCallSchedule.ServiceCallScheduleIndex)]
        public ActionResult ServiceCallScheduleIndex()
        {
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ServiceCallSchedule.ServiceCallScheduleSelect)]
        public JsonResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var referenceModel = new ServiceCallScheduleListModel(request);

            int totalCnt = 0;

            var list = _serviceCallScheduleBl.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = list,
                Total = totalCnt
            });
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.ServiceCallSchedule.ServiceCallScheduleUpdate)]
        public ActionResult ServiceCallScheduleUpdate(int serviceId)
        {
            var bl = new ServiceCallScheduleBL();
            var filter = new ServiceCallScheduleViewModel { ServiceId = serviceId };
            var model = bl.Get(UserManager.UserInfo, filter).Model;
            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.ServiceCallSchedule.ServiceCallScheduleUpdate)]
        public ActionResult ServiceCallScheduleUpdate(ServiceCallScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                _serviceCallScheduleBl.Update(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);
            }

            return PartialView(model);
        }
    }
}
