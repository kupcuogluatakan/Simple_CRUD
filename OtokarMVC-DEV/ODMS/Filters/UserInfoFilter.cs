using System.Web.Mvc;
using System.Web.Routing;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Menu;
using ODMSModel.Permission;

namespace ODMS.Filters
{
    public class UserInfoFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var uInfo = UserManager.UserInfo;

            var rd = filterContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            string currentArea = rd.Values["area"] as string;

            if (currentController == "SystemAdministration" && currentAction == "DoLogout")
                return;

            if (uInfo != null && !uInfo.IsPasswordSet && (currentController != "User" && currentAction != "UserChangePassword"))
            {
                filterContext.Result = new RedirectToRouteResult
                    (
                    new RouteValueDictionary
                        (
                        new
                        {
                            controller = "User",
                            action = "UserChangePassword"
                        }
                        )
                    );
            }

            if (uInfo != null)
            {
                var menuModel = new MenuIndexViewModel
                {
                    Controller = currentController,
                    Action = currentAction
                };
                var menuBo = new MenuBL();
                menuBo.GetMenu(UserManager.UserInfo, menuModel);
                if (menuModel.MenuId != 0)
                {
                    var permissionModel = new PermissionIndexViewModel
                    {
                        PermissionId = menuModel.PermissionId.GetValue<int>()
                    };
                    var permissionBo = new PermissionBL();
                    permissionBo.GetPermission(UserManager.UserInfo, permissionModel);
                    if (permissionModel.IsOtokarScreen && uInfo != null && uInfo.DealerID == 0)
                    {
                        filterContext.Controller.TempData["IsActiveDealerComboBoxVisible"] = permissionModel.IsOtokarScreen;

                        if (uInfo.ActiveDealerId == 0)
                        {
                            filterContext.Controller.TempData["DealerNotSelected"] = true;
                        }
                    }
                }
                if (currentController == "User" && currentAction == "UserChangePassword")
                {
                    filterContext.Controller.TempData["IsActiveDealerComboBoxVisible"] = false;
                    filterContext.Controller.TempData["DealerNotSelected"] = false;
                }
            }

        }
    }
}