using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using Quartz;
using System;
using System.Threading.Tasks;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class ControlClaimPeriodPartListApproveJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CLAIM_PERIOD_PART_LIST_CONTROL, "Başladı");

                var serviceCallSchedule = serviceCallScheduleBL.Get("CLAIM_PERIOD_PART_LIST_CONTROL").Model;
                base.StartService(serviceCallSchedule);
                if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                    return Task.CompletedTask;

                var bl = new ClaimRecallPeriodBL();
                bl.ControlClaimPeriodPartListApprove();

                base.EndService(serviceCallSchedule);

                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CLAIM_PERIOD_PART_LIST_CONTROL, "Bitti");
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CLAIM_PERIOD_PART_LIST_CONTROL, "Hata : " + ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
