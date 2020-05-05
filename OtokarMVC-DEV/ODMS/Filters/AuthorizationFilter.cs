using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using ODMS.Security;
using ODMSCommon.Security;

namespace ODMS.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizationFilter : AuthorizeAttribute
    {
        private readonly string[] _actionPermissionCodes;

        public AuthorizationFilter()
        {

        }
        
        public AuthorizationFilter(params string[] actionPermissionCodes)
        {
            _actionPermissionCodes = actionPermissionCodes;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var rd = httpContext.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            string currentArea = rd.Values["area"] as string;
            //excel export için eklendi
            if (httpContext.Request.UrlReferrer == null && !string.IsNullOrEmpty(httpContext.Request.Headers.Get("UseExcelExport")))
            {
                SetUserInternal(httpContext);
               // return true;
            }

            if ((currentController == "Home" && currentAction == "Index") || (currentController == "SystemAdministration" && currentAction == "NoAuthorization"))
            {
                if (UserManager.UserInfo != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var userPermManager = new UserPermissionManager();
                List<PermissionInfo> listOfPermissions = userPermManager.UserPermissions;
                bool isAuthorized = (_actionPermissionCodes == null) || (_actionPermissionCodes.All(
                    permissionCode =>
                    listOfPermissions.Exists(v => v.PermissionCode == permissionCode))
                    );
                
                return isAuthorized;
            }
        }
       
        private void SetUserInternal(HttpContextBase httpContext)
        {
            var authCookie = httpContext.Request.Headers[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                // Get the forms authentication ticket.
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie);

                //değişebilir
                var identity = new GenericIdentity(authTicket.Name, "Forms");
                var principal = new ODMSUserPrincipal(identity);

                // Get the custom user data encrypted in the ticket.
                var userData = authTicket.UserData;


                //principal.UserInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ODMSModel.DomainModel.UserModel>(userData, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                principal.UserInfo =
                    JsonConvert.DeserializeObject<UserInfo>(userData);
                //(ODMSModel.DomainModel.UserModel)serializer.Deserialize(userData, typeof(ODMSModel.DomainModel.UserModel));

                // Set the context user.
                httpContext.User = principal;
                
                //var coo = httpContext.Request.Cookies.Get("ASP.NET_SessionId");
                //if (coo != null)
                //{
                //    coo.Value = httpContext.Request.Headers["ASP.NET_SessionId"];
                //}

                //set language
                var lang = httpContext.Request.Headers["ODMSApplicationLang"];
                if (!string.IsNullOrEmpty(lang) || !string.IsNullOrWhiteSpace(lang))
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
                }


            }
        }

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);
        //    base.OnAuthorization(filterContext);
        //}

        //private static bool SkipAuthorization(HttpActionContext actionContext)
        //{
        //    Contract.Assert(actionContext != null);

        //    return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
        //           || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        //}
        
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (_actionPermissionCodes.Length != 0)
            {
                var rd = filterContext.HttpContext.Request.RequestContext.RouteData;
                string currentAction = rd.GetRequiredString("action");
                string currentController = rd.GetRequiredString("controller");

                if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != currentController &&
                    filterContext.ActionDescriptor.ActionName != currentAction
                    )
                    base.HandleUnauthorizedRequest(filterContext);
                else
                {
                    if ((currentController == "Home" && currentAction == "Index") || UserManager.UserInfo == null)
                    {
                        filterContext.Result = new RedirectResult("~/SystemAdministration/Login");
                    }
                    //TODO: hata olduğunda buraya düşmemmeli
                    else
                    {
                        HttpContext.Current.Session["PermissionCode"] = _actionPermissionCodes;
                        filterContext.Result = new RedirectResult("~/SystemAdministration/NoAuthorization");
                    }
                }
            }
            else
                base.HandleUnauthorizedRequest(filterContext);
        }

    }
}