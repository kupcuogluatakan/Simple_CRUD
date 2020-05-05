using System.Diagnostics;

namespace ODMSService
{
    public class LoggingHelper
    {
        private string Application;
        private string EventLogName;

        public LoggingHelper(string app, string log)
        {
            Application = app;
            EventLogName = log;

            // Create the event log if it doesn't exist
            if (!EventLog.SourceExists(Application))
            {
                EventLog.CreateEventSource(Application, EventLogName);
            }

        }
        public void WriteToEventLog(string message, string type)
        {
            switch (type.ToUpper())
            {
                case "INFO":
                    EventLog.WriteEntry(Application, message, EventLogEntryType.Information);
                    break;
                case "ERROR":
                    EventLog.WriteEntry(Application, message, EventLogEntryType.Error);
                    break;
                case "WARN":
                    EventLog.WriteEntry(Application, message, EventLogEntryType.Warning);
                    break;
                default:
                    EventLog.WriteEntry(Application, message, EventLogEntryType.Information);
                    break;
            }
        }

    }

}
