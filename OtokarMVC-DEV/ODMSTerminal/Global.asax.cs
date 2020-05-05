using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ODMSCommon.Security;
using Newtonsoft.Json;
using Autofac;
using Autofac.Integration.Mvc;
using ODMSTerminal.Infrastructure;

namespace ODMSTerminal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IContainer _container;

        protected void Application_Start()
        {
            _container = IoC.LetThereBeIoC();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var identity = new GenericIdentity(authTicket.Name, "Forms");
                var principal = new ODMSUserPrincipal(identity);
                var userData = ((FormsIdentity)(Context.User.Identity)).Ticket.UserData;
                principal.UserInfo = JsonConvert.DeserializeObject<UserInfo>(userData);
                Context.User = principal;
            }
        }

        protected void Application_End()
        {
            var container = _container;
            if (container != null) container.Dispose();
        }
    }
}
