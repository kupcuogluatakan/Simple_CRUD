using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.VehicleCode;
using ODMSService.Helpers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class VehicleCodeJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("VEHICLE_CODE_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var bl = new VehicleCodeBL();

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
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "VEHICLE_CODE_SERVICE";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_CODE_SERVICE, "ZDMS_VEHICLE_CODE servisi çağırılıyor");
                    var rValue = pssc.ZDMS_VEHICLE_CODE(UserName, Password, null);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_CODE_SERVICE, "ZDMS_VEHICLE_CODE servisi bitti");

                    XmlNodeList xmlList = rValue.SelectNodes("//tbl");
                    var listModel = (from XmlNode xmlNode in xmlList
                                     select new VehicleCodeXMLViewModel
                                     {
                                         CodeSSID = xmlNode["CODE_SSID"].InnerText,
                                         AdminDesc = xmlNode["ADMIN_DESC"].InnerText,
                                         TypeSSID = xmlNode["TYPE_SSID"].InnerText,
                                         ModelSSID = xmlNode["MODEL_SSID"].InnerText,
                                         EngineType = xmlNode["MOTOR_SSID"].InnerText,
                                         CommandType = CommonValues.DMLType.Insert
                                     }).ToList();

                    XmlNodeList xmlLangList = rValue.SelectNodes("//tbl2");
                    var listLangModel = (from XmlNode xmlNode in xmlLangList
                                         select new VehicleCodeLangXMLModel
                                         {
                                             CodeSSID = xmlNode["CODE_SSID"].InnerText,
                                             LanguageCode = xmlNode["LANGUAGE_CODE"].InnerText,
                                             CodeName = xmlNode["CODE_NAME"].InnerText
                                         }).ToList();

                    #region db process

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_CODE_SERVICE, "XMLtoDBVehicleCode metodu çağırılıyor");

                    var re = bl.XMLtoDBVehicleCode(listModel, listLangModel);
                    serviceCallLog.IsSuccess = re.IsSuccess;
                    serviceCallLog.ErrorMessage = re.Message;

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_CODE_SERVICE, "XMLtoDBVehicleCode metodu bitti");

                    #endregion

                    #region Response & Error XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.LogType = CommonValues.LogType.Response;
                        serviceCallLog.LogXml = new XElement(XDocument.Parse(rValue.OuterXml).Root);
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
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_CODE_SERVICE, string.Format("Hata : {0}", ex.Message));
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