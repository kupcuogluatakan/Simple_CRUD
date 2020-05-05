using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ODMSCommon.Security;

namespace ODMSTerminal.Infrastructure.MvcActionFilters
{
    public class LocalizationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserManager.UserInfo != null)
            {
                var cultureInfo = GetCultureInfo(UserManager.UserInfo.LanguageCode.ToLower());
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            base.OnActionExecuting(filterContext);
        }

        private static CultureInfo GetCultureInfo(string culture)
        {
            CultureInfo ci;
            switch (culture)
            {
                case "en":
                    ci= new CultureInfo("en-US");
                    ci.DateTimeFormat.DateSeparator = "-";
                    break;
                default:
                    ci = new CultureInfo("tr-TR");
                    ci.DateTimeFormat.DateSeparator = "/";
                    break;
            }

            return ci;
        }
    }
}