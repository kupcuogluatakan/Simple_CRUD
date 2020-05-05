using System;
using System.Threading;
using System.Web;

namespace ODMS.Core
{
    public class CookieLocalizationModule : IHttpModule
    {
        private HttpApplication _application;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            _application = context;
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            _application.Context.Response.Headers.Remove("Server");
            _application.Context.Response.Headers.Remove("X-AspNet-Version");
            _application.Context.Response.Headers.Remove("X-AspNetMvc-Version");
            _application.Context.Response.Headers.Remove("X-Powered-By");

            if (HttpContext.Current.Request.Cookies["ODMSApplicationLang"] == null)
                return;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(HttpContext.Current.Request.Cookies["ODMSApplicationLang"].Value);
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(HttpContext.Current.Request.Cookies["ODMSApplicationLang"].Value);
        }
    }
}