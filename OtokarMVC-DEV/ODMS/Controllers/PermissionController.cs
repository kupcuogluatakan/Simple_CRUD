using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSModel.Permission;
using ODMSModel.Shared;
using ODMSBusiness;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PermissionController : ControllerBase
    {
        #region Permission Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Permission.PermissionIndex)]
        [HttpGet]
        public ActionResult PermissionIndex()
        {
            return View();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Permission.PermissionIndex, ODMSCommon.CommonValues.PermissionCodes.Permission.PermissionDetails)]
        public ActionResult ListPermission([DataSourceRequest]DataSourceRequest request, PermissionListModel model)
        {
            var permissionBo = new PermissionBL();
            var v = new PermissionListModel(request);
            var totalCnt = 0;
            v.PermissionCode = model.PermissionCode;
            v.PermissionName = model.PermissionName;
            var returnValue = permissionBo.ListPermissions(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region Permission Details

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Permission.PermissionIndex, ODMSCommon.CommonValues.PermissionCodes.Permission.PermissionDetails)]
        [HttpGet]
        public ActionResult PermissionDetails(int id = 0)
        {
            var v = new PermissionIndexViewModel();
            var roleBo = new PermissionBL();

            v.PermissionId = id;
            roleBo.GetPermission(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}
