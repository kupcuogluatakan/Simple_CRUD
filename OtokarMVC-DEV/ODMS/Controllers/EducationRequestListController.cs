using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSModel.EducationRequests;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class EducationRequestListController : ControllerBase
    { 
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationRequest.EducationRequestIndex)]
        public ActionResult EducationRequestListIndex(string id)
        {
            ViewBag.EducationCode = id;
            if (id==null)
            {
                ViewBag.HideElements = true;
                return PartialView();
            }
            ViewBag.HideElements = false;
            return PartialView();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationRequest.EducationRequestIndex)]
        public JsonResult ListEducationRequests([DataSourceRequest]DataSourceRequest request,string educationCode)
        {
            var bus = new EducationRequestListBL();
            var model = new EducationRequestsListModel(request) {EducationCode = educationCode};

            var totalCnt = 0;
            var returnValue = bus.GetEducationRequests(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
    }
}
