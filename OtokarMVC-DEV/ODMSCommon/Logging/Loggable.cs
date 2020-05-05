using System;
using System.Threading.Tasks;
using NLog;

namespace ODMSCommon.Logging
{
    public class Loggable
    {
        protected readonly ILogger Logger;

        public Loggable()
        {
            Logger = LoggerAccessor.Logger(GetType());
        }

        /// <summary>
        /// Use this method when you cannot extend from Loggable for some reason
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILogger GetLoggerInstance(Type type)
        {
            return LoggerAccessor.Logger(type);
        }
        #region ASync Log Methods
        public void DebugAsync(string message)
        {
            new Task(()=> Logger.Debug(message)).Start();
        }
        public void ErrorAsync(string message)
        {
            new Task(() => Logger.Error(message)).Start();
        }
        public void InfoAsync(string message)
        {
            new Task(() => Logger.Info(message)).Start();
        }
        public void FatalAsync(string message)
        {
            new Task(() => Logger.Fatal(message)).Start();
        }
        public void WarnAsync(string message)
        {
            new Task(() => Logger.Warn(message)).Start();
        }
        public void LogException(System.Exception ex)
        {
            Logger.Error(ex);
        }

        #endregion
    }
}
