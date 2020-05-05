using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Shared;
using ODMSModel.RolePermission;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class RolePermissionController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.RolePermission.RolePermissionIndex)]
        public ActionResult RolePermissionIndex()
        {
            var model = new RolePermissionBL().ListRoles(UserManager.UserInfo).Data;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.RolePermission.RolePermissionIndex)]
        public ActionResult ListPermissionsIncludedInRole([DataSourceRequest] DataSourceRequest request, int? roleId)
        {
            if (roleId.HasValue)
            {
                var bo = new RolePermissionBL();
                var result = bo.ListPermissionsIncludedInRole(UserManager.UserInfo, roleId.GetValue<int>()).Data;

                return Json(new
                {
                    Data = result
                });
            }
            else
            {
                return Json(new
                {
                    Data = new List<PermissionListModel>()
                });
            }
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.RolePermission.RolePermissionIndex)]
        public ActionResult ListPermissionsNotIncludedInRole([DataSourceRequest] DataSourceRequest request, int? roleId)
        {
            if (roleId.HasValue)
            {
                var bo = new RolePermissionBL();
                var result = bo.ListPermissionsNotIncludedInRole(UserManager.UserInfo, roleId.GetValue<int>()).Data;

                return Json(new
                {
                    Data = result
                });
            }
            else
            {
                return Json(new
                {
                    Data = new List<PermissionListModel>()
                });
            }
        }

        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.RolePermission.RolePermissionIndex, CommonValues.PermissionCodes.RolePermission.RolePermissionSave)]
        public ActionResult Save(SaveModel model)
        {
            var bo = new RolePermissionBL();
            try
            {
                bo.Save(UserManager.UserInfo, model);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            catch
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.RolePermission_Save_Error);
            }
        }
    }
}