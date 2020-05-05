using System;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using FluentValidation.Mvc;
using FluentValidation.Validators;
using Newtonsoft.Json;
using ODMS.Core;
using ODMSCommon.Exception;
using ODMSCommon.Security;
using ODMSModel.CustomValidators;
using ODMS.Container;
using System.Diagnostics;
using System.Collections.Generic;
using ODMSModel.User;

namespace ODMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var httpApp = (HttpApplication)sender;
            if (httpApp.Request.RequestType != "POST" && !httpApp.Request.Path.Contains("/Reports/")) return;

            var timer = new Stopwatch();
            httpApp.Context.Items["Timer"] = timer;
            httpApp.Context.Items["HeadersSent"] = false;
            timer.Start();

            if (httpApp.Request.RequestType == "GET")
            {
                HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
                RouteData rd = RouteTable.Routes.GetRouteData(context);
                if (rd != null)
                {
                    string controllerName = rd.GetRequiredString("controller");
                    string actionName = rd.GetRequiredString("action");

                }
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var httpApp = (HttpApplication)sender;
            if (httpApp.Request.RequestType != "POST" && !httpApp.Request.Path.Contains("/Reports/") && !httpApp.Request.Path.Contains("/Price/")) return;

            HttpContext httpContext = HttpContext.Current;
            RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
            if (requestContext.HttpContext.Request.IsAjaxRequest())
            {
                var timer = (Stopwatch)httpApp.Context.Items["Timer"];
                if (timer != null)
                {
                    timer.Stop();
                    if (!(bool)httpApp.Context.Items["HeadersSent"])
                    {
                        httpApp.Context.Response.AppendHeader("ResponseTime", timer.Elapsed.Seconds.ToString());
                    }
                }
            }

            httpApp.Context.Items.Remove("Timer");
            httpApp.Context.Items.Remove("HeadersSent");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                // Get the forms authentication ticket.
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                //değişebilir
                var identity = new GenericIdentity(authTicket.Name, "Forms");
                var principal = new ODMSUserPrincipal(identity);

                // Get the custom user data encrypted in the ticket.
                var userData = ((FormsIdentity)(Context.User.Identity)).Ticket.UserData;


                //principal.UserInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ODMSModel.DomainModel.UserModel>(userData, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                principal.UserInfo =
                    JsonConvert.DeserializeObject<UserInfo>(userData);
                //(ODMSModel.DomainModel.UserModel)serializer.Deserialize(userData, typeof(ODMSModel.DomainModel.UserModel));

                // Set the context user.
                Context.User = principal;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var err = Server.GetLastError();
            if (err is ODMSDatabaseConnectionException)
            {
                Server.ClearError();
                HttpContext.Current.Response.Redirect("~/SystemAdministration/NoDbConnection");
            }
            else if (err is HttpRequestValidationException)
            {
                //redirect to a static page and show proper error message
                Server.ClearError();
                HttpContext.Current.Response.Redirect("~/SystemAdministration/DangerousInput");
            }
        }

        protected void Application_Start()
        {
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Resource";
            DefaultModelBinder.ResourceClassKey = "Resource";
            AreaRegistration.RegisterAllAreas();

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new NullableDateTimeBinder());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //FluentValidation Configuration
            FluentValidationModelValidatorProvider.Configure(x =>
                x.Add(typeof(GreaterThanOrEqualValidator),
                    (metadata, Context, rule, validator) =>
                        new CustomGreaterThenOrEqualTo
                            (
                            metadata, Context, rule, validator
                            )
                    ));



            //AuthConfig.RegisterAuth();

            //OZAN : bundle yaparsanız, development ortamında bilindik yöntem ile javascript debug edemezsiniz. google it.
            //debug mode'da da bundle yapmaya force etmek için
            //BundleTable.EnableOptimizations = false;

            //(new LogConfig()).Register();

            //ViewEngine Register Taner

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ODMSViewEngine());

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            Application["OnlineUserSessionList"] = new List<string>();
            Application["TotalOnlineUsers"] = 0;
        }


        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started  
            Application.Lock();
            var sessionId = Session.SessionID;
            (Application["OnlineUserSessionList"] as List<string>).Add(sessionId);
            Application["TotalOnlineUsers"] = (int)Application["TotalOnlineUsers"] + 1;
            Application.UnLock();
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.   
            // Note: The Session_End event is raised only when the sessionstate mode  
            // is set to InProc in the Web.config file. If session mode is set to StateServer   
            // or SQLServer, the event is not raised.  
            Application.Lock();
            var sessionId = Session.SessionID;
            (Application["OnlineUserSessionList"] as List<string>).Remove(sessionId);
            Application["TotalOnlineUsers"] = (int)Application["TotalOnlineUsers"] - 1;
            Application.UnLock();
        }
    }
}