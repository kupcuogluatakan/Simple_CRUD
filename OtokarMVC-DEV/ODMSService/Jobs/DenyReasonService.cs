using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using System;

namespace ODMSService.ServiceClasses
{
    internal class DenyReasonService : ServiceBase
    {
        private ServiceScheduleModel serviceModel = new ServiceScheduleModel();
        public DenyReasonService(ServiceScheduleModel model)
        {
            serviceModel = model;
        }

        public override void Execute()
        {
            var serviceCallSchedule = base.StartService(serviceModel.Id);
            var serviceCallLog = new ServiceCallLogModel();
            var responseLogModel = new ServiceCallLogModel(serviceCallSchedule.TriggerServiceName);
            var poDetBl = new PurchaseOrderDetailBL();

            var lastSuccessCallDateTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd HH:mm:ss") : DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            var lastSuccessCallDate = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
            var lastSuccessCallTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("HH:mm:ss") : DateTime.Now.ToString("HH:mm:ss");

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
                responseLogModel.LogId = serviceCallLog.LogId;
            }

            #endregion

            using (var pssc = GetClient())
            {
                #region Calling Deny Reason Service & Filling Model


                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.DENY_REASON_SERVICE, "ZDMS_RED_NEDENI_BILGILERI servisi çağırılıyor");
                DataSet rValue = pssc.ZDMS_RED_NEDENI_BILGILERI(UserName, Password, lastSuccessCallDate, null);
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.DENY_REASON_SERVICE, "ZDMS_RED_NEDENI_BILGILERI servisi bitti");

                DataTable dtMaster = rValue.Tables["Table1"];
                if (dtMaster != null)
                {
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.DENY_REASON_SERVICE, "SaveDenyReasons metodu çağırılıyor");
                    responseLogModel = poDetBl.SaveDenyReasons(dtMaster);
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

            if (!responseLogModel.IsSuccess)
            {
                base.SendErrorMail(responseLogModel);
            }

            base.EndService(serviceCallSchedule);
        }
    }
}
