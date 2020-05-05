using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using System;
using Quartz;
using System.Threading.Tasks;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class DenyReasonJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("DENY_REASON_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var poDetBl = new PurchaseOrderDetailBL();

            var lastSuccessCallDateTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd HH:mm:ss") : DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            var lastSuccessCallDate = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
            var lastSuccessCallTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("HH:mm:ss") : DateTime.Now.ToString("HH:mm:ss");

            try
            {
                using (var pssc = GetClient())
                {
                    #region Request XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.ReqResDic = new Dictionary<string, string>()
                                        {
                                            {"DateTime", lastSuccessCallDateTime}
                                        };
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "DENY_REASON_SERVICE";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion

                    #region Calling Deny Reason Service & Filling Model


                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.DENY_REASON_SERVICE, "ZDMS_RED_NEDENI_BILGILERI servisi çağırılıyor");
                    DataSet rValue = pssc.ZDMS_RED_NEDENI_BILGILERI(UserName, Password, lastSuccessCallDate, null);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.DENY_REASON_SERVICE, "ZDMS_RED_NEDENI_BILGILERI servisi bitti");

                    DataTable dtMaster = rValue.Tables["Table1"];
                    if (dtMaster != null)
                    {
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.DENY_REASON_SERVICE, "SaveDenyReasons metodu çağırılıyor");

                        var re = poDetBl.SaveDenyReasons(dtMaster);
                        serviceCallLog.IsSuccess = re.IsSuccess;
                        serviceCallLog.ErrorMessage = re.ErrorMessage;
                        serviceCallLog.ErrorModel = re.ErrorModel;

                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.DENY_REASON_SERVICE, "SaveDenyReasons metodu bitti");
                    }

                    #endregion

                    #region Response & Error XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.LogType = CommonValues.LogType.Response;
                        serviceCallLog.LogXml = XElement.Parse(rValue.ToXml());
                        base.LogResponseService(serviceCallLog);
                    }

                    #endregion
                }

                if (!serviceCallLog.IsSuccess)
                {
                    base.SendErrorMail(serviceCallLog);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.DENY_REASON_SERVICE, string.Format("Hata : {0}", ex.Message));

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
