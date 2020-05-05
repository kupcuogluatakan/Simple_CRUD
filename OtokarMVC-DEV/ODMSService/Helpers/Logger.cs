using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Helpers
{
    public enum LoggerType
    {
        File
    }

    public enum ServiceType
    {
        GENERAL,
        VEHICLE_GROUP_SERVICE,
        VEHICLE_MODEL_SERVICE,
        VEHICLE_TYPE_SERVICE,
        VEHICLE_CODE_SERVICE,
        SPART_INFO_SERVICE,
        SPARE_PART_SPLITTING,
        RETAIL_PRICE_SERVICE,
        SPARE_PART_SERVICE,
        REPORT_CREATE_SERVICE,
        GIF_SAVE,
        MRP_CONTROL,
        VEHICLE_SERVICE,
        VEHICLE_SALES_SERVICE,
        FREE_PO_SERVICE,
        DENY_REASON_SERVICE,
        CLAIM_PERIOD_LAST_DAY_CONTROL,
        CLAIM_PERIOD_PART_LIST_CONTROL,
        ZDMS_YP_ISKONTO,
        VEHICLE_WARRANTY_END_CONTROL,
        CAMPAING_PRICE_SERVICE,
        WORK_ORDER_DAY_CONTROL,
        RUN_IMMEDIATELY,
        WORK_ORDER_HOUR_CONTROL
    }

    public interface ILogger
    {
        void AddLog(LoggerType type, ServiceType serviceType, string message);
    }

    public class Logger : ILogger
    {
        private static object threadlock;
        public Logger()
        {
            threadlock = new object();
        }
        public void AddLog(LoggerType type, ServiceType serviceType, string message)
        {
            try
            {
                var folder = new FileInfo(Assembly.GetEntryAssembly().Location).Directory.ToString();
                string serviceName = Enum.GetName(typeof(ServiceType), serviceType);
                switch (type)
                {
                    case LoggerType.File:
                        lock (threadlock)
                        {
                            File.AppendAllText(string.Format(@"{0}/{1}.txt", folder, serviceName), string.Format("{0} {1}", message, DateTime.Now.ToString()) + Environment.NewLine);
                        }
                        break;
                    default:
                        lock (threadlock)
                        {
                            File.AppendAllText(string.Format(@"{0}/{1}.txt", folder, serviceName), string.Format("{0} {1}", message, DateTime.Now.ToString()) + Environment.NewLine);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public class LoggerFactory
    {
        private static Logger _loggerObjects;
        public static ILogger Logger
        {
            get
            {
                if (_loggerObjects == null)
                    _loggerObjects = new Logger();
                return _loggerObjects;
            }
        }
    }
}
