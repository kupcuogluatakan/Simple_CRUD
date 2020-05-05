using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ActiveAnnouncementsController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ActiveAnnouncements.ActiveAnnouncementsIndex)]
        public ActionResult Index()
        {
            var model = new ActiveAnnouncementsBL().ListActiveAnnouncements(UserManager.UserInfo).Data;
            return View(model);
        }
        
        [ChildActionOnly]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ActiveAnnouncements.ActiveAnnouncementsIndex)]
        public PartialViewResult ActiveSlideAnnouncements()
        {
            var model = new ActiveAnnouncementsBL().ListActiveAnnouncements(UserManager.UserInfo, true).Data;
            return PartialView("~/Views/ActiveAnnouncements/ActiveSlideAnnouncements.cshtml", model);
        }


        [ChildActionOnly]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ActiveAnnouncements.ActiveAnnouncementsIndex)]
        public ActionResult ActiveAnnouncementsLink()
        {
            int newMessageCount = 0;
            var model = new ActiveAnnouncementsBL().GetActiveAnnouncementCount(UserManager.UserInfo,out newMessageCount).Model;
            ViewBag.NewMessageCount = newMessageCount;
            return PartialView("_ActiveAnnouncementsLink",model);
        }
    }
}
