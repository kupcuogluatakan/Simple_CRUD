using System;
using System.Collections.Generic;
using ODMSModel.ServiceCallSchedule;
using ODMSBusiness;
using ODMSCommon;
using ODMSService.Helpers;
using Quartz;
using System.Threading.Tasks;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class WorkOrderDetailReportDayControlJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("WORK_ORDER_DAY_CONTROL").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var ReportBl = new ReportsBL();

            var lastSuccessCallDateTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd HH:mm:ss") : DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            var lastSuccessCallDate = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
            var lastSuccessCallTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("HH:mm:ss") : DateTime.Now.ToString("HH:mm:ss");

            try
            {

                #region Request XML Logging

                if (serviceCallSchedule.IsResponseLogged)
                {
                    serviceCallLog.ReqResDic = new Dictionary<string, string>()
                        {
                            {"DateTime", lastSuccessCallDateTime}
                        };
                    serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName;
                    serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                    serviceCallLog.LogType = CommonValues.LogType.Request;
                    base.LogRequestService(serviceCallLog);
                }

                #endregion

                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.WORK_ORDER_DAY_CONTROL, "CheckWorkOrderDayData metodu çağırılıyor");

                string errorMessage = ReportBl.CheckWorkOrderDayData().Model;

                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.WORK_ORDER_DAY_CONTROL, "CheckWorkOrderDayData metodu bitti");

                serviceCallLog.IsSuccess = string.IsNullOrEmpty(errorMessage);
                serviceCallLog.ErrorMessage = errorMessage;

                #region Response & Error XML Logging

                if (serviceCallSchedule.IsResponseLogged)
                {
                    serviceCallLog.LogType = CommonValues.LogType.Response;
                    base.LogResponseService(serviceCallLog);
                }

                #endregion

                if (!serviceCallLog.IsSuccess)
                {
                    base.SendErrorMail(serviceCallLog);
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.WORK_ORDER_DAY_CONTROL, string.Format("Hata : {0}", ex.Message));
                serviceCallLog.ErrorMessage = ex.Message;
                base.LogResponseService(serviceCallLog);
            }
            finally
            {
                base.EndService(serviceCallSchedule);
            }

            return Task.CompletedTask;
        }
    }
}
