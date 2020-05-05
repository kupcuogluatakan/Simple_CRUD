using ODMSBusiness;
using ODMSModel.WebServiceErrorDisplay;
using ODMS.Filters;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Common;
using ODMSCommon.Security;
using System.Web.Mvc;
using Permission = ODMSCommon.CommonValues.PermissionCodes.WebServiceError;

namespace ODMS.Controllers
{
    public class WebServiceErrorDisplayController : ControllerBase
    {
        WebServiceErrorDisplayBL _webServiceErrorDisplayBL = new WebServiceErrorDisplayBL();

        [HttpGet]
        [AuthorizationFilter(Permission.WebServiceErrorIndex)]
        public ActionResult WebServiceErrorDisplayIndex(int? id)
        {
            WebServiceErrorDisplayViewModel model = new WebServiceErrorDisplayViewModel();

            if (id.HasValue)
            {
                model.ServiceLogId = id.Value;
                model = _webServiceErrorDisplayBL.Get(UserManager.UserInfo, model).Model;

                if (model.ErrorNo != 0)
                {
                    CheckErrorForMessage(model, false);
                    return View();
                }
            }

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.WebServiceErrorIndex)]
        public ActionResult WebServiceErrorDisplayIndex(WebServiceErrorDisplayViewModel model)
        {
            return RedirectToAction("WebServiceErrorDisplayIndex", new { id = model.ServiceLogId });
        }

        [HttpGet]
        public ActionResult WebServiceErrorDisplayDetail(int id, XmlErrorType type)
        {
            WebServiceErrorDisplayViewModel model = new WebServiceErrorDisplayViewModel() { ServiceLogId = id };

            if (id != 0)
                _webServiceErrorDisplayBL.GetDetail(model, type);

            return PartialView(model);
        }
    }
}
