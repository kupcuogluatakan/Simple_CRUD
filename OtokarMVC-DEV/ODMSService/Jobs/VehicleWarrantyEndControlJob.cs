using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using ODMSCommon;
using Quartz;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class VehicleWarrantyEndControlJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("VEHICLE_WARRANTY_END_CONTROL").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel(serviceCallSchedule.TriggerServiceName);
            var vehicleBl = new VehicleBL();

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
                    serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName;// "VEHICLE_TYPE_SERVICE";
                    serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                    serviceCallLog.LogType = CommonValues.LogType.Request;
                    base.LogRequestService(serviceCallLog);
                }

                #endregion

                var re = vehicleBl.UpdateWarrantyEndStatuses();
                serviceCallLog.IsSuccess = re.IsSuccess;
                serviceCallLog.ErrorMessage = re.Message;

                #region Response & Error XML Logging

                if (serviceCallSchedule.IsResponseLogged)
                {
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
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_WARRANTY_END_CONTROL, string.Format("Hata : {0}", ex.Message));
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