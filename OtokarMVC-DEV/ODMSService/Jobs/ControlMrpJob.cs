using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using Quartz;
using System;
using System.Threading.Tasks;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class ControlMrpJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.MRP_CONTROL, "Başladı");
            try
            {
                var serviceCallSchedule = serviceCallScheduleBL.Get("MRP_CONTROL").Model;
                base.StartService(serviceCallSchedule);
                if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                    return Task.CompletedTask;

                var bl = new PurchaseOrderSuggestionDetailBL();
                bl.ControlMrP();

                base.EndService(serviceCallSchedule);
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.MRP_CONTROL, "Hata : " + ex.Message);
            }
            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.MRP_CONTROL, "Bitti");
            return Task.CompletedTask;
        }
    }
}
