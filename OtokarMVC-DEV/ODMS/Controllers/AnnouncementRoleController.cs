using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.AnnouncementRole;
using System;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class AnnouncementRoleController : ControllerBase
    {

        [AuthorizationFilter(CommonValues.PermissionCodes.AnnouncementRole.AnnouncementRoleIndex, CommonValues.PermissionCodes.AnnouncementRole.AnnouncementRoleIndex)]
        public ActionResult ListAnnouncementRole([DataSourceRequest] DataSourceRequest request, AnnouncementRoleListModel model)
        {
            var announcementRoleBo = new AnnouncementRoleBL();
            var filter = new AnnouncementRoleListModel(request) { IdAnnouncement = model.IdAnnouncement };
            var response = announcementRoleBo.ListAnnouncementRole(UserManager.UserInfo, filter);
            return Json(response);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.AnnouncementRole.AnnouncementRoleIndex, CommonValues.PermissionCodes.AnnouncementRole.AnnouncementRoleIndex)]
        public ActionResult ListRoleTypeWithoutAnnouncement([DataSourceRequest] DataSourceRequest request, AnnouncementRoleListModel model)
        {
            var announcementRoleBo = new AnnouncementRoleBL();
            var filter = new AnnouncementRoleListModel(request) { IdAnnouncement = model.IdAnnouncement };
            var response = announcementRoleBo.ListRoleTypeWithoutAnnouncement(UserManager.UserInfo, filter);
            return Json(response);
        }

        public ActionResult AnnouncementRoleIndex(Int64 IdAnnouncement, int isActive)
        {
            ViewBag.MasterIsActive = isActive;
            return View(IdAnnouncement);
        }

        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.AnnouncementRole.AnnouncementRoleIndex, CommonValues.PermissionCodes.AnnouncementRole.AnnouncementRoleUpdate)]
        public ActionResult SaveAnnouncementRole(AnnouncementRoleSaveModel model)
        {
            var bo = new AnnouncementRoleBL();
            try
            {
                bo.SaveAnnouncementRole(UserManager.UserInfo, model);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            catch
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Global_Display_Error);
            }
        }

    }
}