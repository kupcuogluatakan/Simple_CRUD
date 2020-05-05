using ODMSCommon.Logging;
using log4net;

namespace ODMS
{
    public class LogConfig : Loggable
    {
        public void Register()
        {
            GlobalContext.Properties["LogFileName"] = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/Logs/odms.web.log"));
            log4net.Config.XmlConfigurator.Configure();

            //Logger.Info("Logger initialized"); 
        }
    }
}