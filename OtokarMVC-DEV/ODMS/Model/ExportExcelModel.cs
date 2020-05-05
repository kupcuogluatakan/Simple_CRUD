using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ODMS.Model
{
    public class ExportModel
    {
        public static string ExcelDownloadPath
        {
            get
            {
                var request = HttpContext.Current.Request;
                var appUrl = HttpRuntime.AppDomainAppVirtualPath;

                if (appUrl != "/")
                    appUrl = "/" + appUrl;

                var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
                if (ConfigurationManager.AppSettings["ExcelDownloadBasePath"] != null)
                {
                    baseUrl = ConfigurationManager.AppSettings["ExcelDownloadBasePath"].ToString();
                }

                return baseUrl;
            }
        }
        public string ButtonText { get; set; }
        public string JsFunctionName { get; set; }
    }
}