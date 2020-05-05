using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.RolePermission;
using ODMSModel.UserRole;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    //TODO: add autorization atrributes
    [PreventDirectFilter]
    public class UserRoleController:ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.UserRole.UserRoleIndex)]
        public ActionResult UserRoleIndex()
        {
            var model = new UserRoleViewModel();
            var boUserRole = new UserRoleBL();
            model.UserList = boUserRole.GetUsersList(UserManager.UserInfo).Data;
            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.UserRole.UserRoleIndex)]
        public JsonResult ListRolesIncludedInUser(string userId)
        {
            return Json(new {Data = new UserRoleBL().GetUserRolesIncluded(UserManager.UserInfo, userId).Data});
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.UserRole.UserRoleIndex)]
        public JsonResult ListRolesNotIncludedInUser(string userId)
        {
            return Json(new { Data = new UserRoleBL().GetUserRolesExcluded(UserManager.UserInfo, userId).Data });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.UserRole.UserRoleIndex, CommonValues.PermissionCodes.UserRole.UserRoleSave)]
        public JsonResult Save(SaveModel model)
        {
            var bo = new UserRoleBL();
            bo.Save(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);

        }
    }
}