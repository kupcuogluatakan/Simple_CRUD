using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSModel.ServiceCallSchedule;
using ODMSBusiness;
using ODMSCommon;
using System.Xml;
using ODMSModel.SpartInfo;
using ODMSBusiness.Business;
using System.Xml.Linq;
using System.Data;
using ODMSService.Helpers;
using Quartz;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class PartClassInfoJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("SPART_INFO_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var bl = new SpartInfoBL();

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
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; //ZDMS_BOLUM_BILGILERI
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPART_INFO_SERVICE, "ZDMS_BOLUM_BILGILERI servisi çağırılıyor");
                    DataSet rValue = pssc.ZDMS_BOLUM_BILGILERI(UserName, Password);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPART_INFO_SERVICE, "ZDMS_BOLUM_BILGILERI servisi bitti");

                    DataTable dtMain = new DataTable();
                    dtMain = rValue.Tables["Table1"];

                    var listModel = dtMain.AsEnumerable().Select(rowMain => new SpartInfoXMLViewModel
                    {
                        Language = rowMain.Field<string>("LAISO"),
                        Spart = rowMain.Field<string>("SPART"),
                        Text = rowMain.Field<string>("VTEXT"),
                        CommandType = CommonValues.DMLType.Insert
                    }).ToList();

                    var langListModel = listModel.AsEnumerable().Select(rowLang => new SpartInfoXMLViewModel
                    {
                        Language = rowLang.Language,
                        Spart = rowLang.Spart,
                        Text = rowLang.Text,
                        CommandType = CommonValues.DMLType.Insert
                    }).ToList();

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPART_INFO_SERVICE, "XMLtoDBSPartInfo metodu çağırılıyor");

                    var re = bl.XMLtoDBSPartInfo(listModel, langListModel);
                    serviceCallLog.IsSuccess = re.IsSuccess;
                    serviceCallLog.ErrorMessage = re.ErrorMessage;
                    serviceCallLog.ErrorModel = re.ErrorModel;

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPART_INFO_SERVICE, "XMLtoDBSPartInfo metodu bitti");

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
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPART_INFO_SERVICE, string.Format("Hata : {0}", ex.Message));

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
