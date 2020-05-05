using System;
using NLog;

namespace ODMSCommon.Logging
{
    public static class LoggerAccessor
    {
        public static ILogger Logger(Type type)
        {
            var logger = LogManager.GetCurrentClassLogger();
            return logger;
        }
    }
}
