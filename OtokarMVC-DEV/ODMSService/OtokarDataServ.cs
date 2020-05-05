using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using ODMSService.Jobs;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;

namespace ODMSService
{
    public partial class OtokarDataServ : System.ServiceProcess.ServiceBase
    {
        public OtokarDataServ()
        {
            InitializeComponent();
#if(DEBUG)
            OnStart(null);
#endif
        }

        private List<Thread> tr = new List<Thread>();

        protected override void OnStart(string[] args)
        {
            LoggingHelper log = new LoggingHelper("ODMSApplication", "ODMSServiceLog");
            log.WriteToEventLog("Started", "INFO");

            try
            {
                JobScheduler.Start();
            }
            catch (System.Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ODMSService.Helpers.ServiceType.GENERAL, ex.Message);
                log.WriteToEventLog(ex.Message, "ERROR");
            }
        }

        protected override void OnStop()
        {
            LoggingHelper log = new LoggingHelper("ODMSApplication", "ODMSServiceLog");
            log.WriteToEventLog("Stopped", "INFO");
           
            foreach (Thread thread in tr)
            {
                thread.Abort();
            }
        }
    }
}