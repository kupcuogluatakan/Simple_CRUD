using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSTerminal.Security;

namespace ODMSTerminal.Infrastructure.MvcActionFilters
{
    public class PermissionAuthorizeAttribute:AuthorizeAttribute
    {
        private readonly string _permissionCode;
        public PermissionAuthorizeAttribute(string permissionCode)
        {
            _permissionCode = permissionCode;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return
                !(UserManager.UserInfo == null ||
                  new UserPermissionManager().UserPermissions.All(c => c.PermissionCode != _permissionCode));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (UserManager.UserInfo == null)
            {
                filterContext.Result = new RedirectResult("~/login");
                return;
            }
            filterContext.Controller.TempData["MessageToShow"] = MessageResource.Global_Warning_NoAuthorization;
            filterContext.Controller.TempData["IsError"] = true;
            filterContext.Result = new RedirectResult("~/error");

        }
    }
}