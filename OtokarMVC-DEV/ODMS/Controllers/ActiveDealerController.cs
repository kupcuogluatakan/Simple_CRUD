using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ActiveDealer;
using System;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ActiveDealerController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.ActiveDealerChoice.ActiveDealerChoiceIndex)]
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        [AuthorizationFilter(CommonValues.PermissionCodes.ActiveDealerChoice.ActiveDealerChoiceIndex)]
        public ActionResult ActiveDealerChoice()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ActiveDealerModel model = new ActiveDealerModel();
            if (UserManager.UserInfo.ActiveDealerId != 0)
            {
                model.DealerId = UserManager.UserInfo.ActiveDealerId;
            }
            return PartialView("_ActiveDealerChoice", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.ActiveDealerChoice.ActiveDealerChoiceIndex)]
        public ActionResult SetDealer(int dealerId)
        {
            var userInfoModified = UserManager.UserInfo;
            userInfoModified.ActiveDealerId = dealerId;

            Response.Cookies["otokar"]["activeDealerId"] = dealerId.ToString();
            Response.Cookies["otokar"]["username"] = userInfoModified.UserName;
            Response.Cookies["otokar"]["password"] = userInfoModified.Password;
            Response.Cookies["otokar"]["language"] = userInfoModified.LanguageCode;
            Response.Cookies["otokar"].Expires = DateTime.Now.AddYears(10);

            CommonUtility.CreateAuthenticationCookie(userInfoModified);
            return Json("");
        }
    }
}
