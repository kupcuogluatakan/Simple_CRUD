using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.Vehicle;
using ODMSService.Helpers;
using Quartz;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class VehicleJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("VEHICLE_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var mainBL = new VehicleBL();

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
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "VEHICLE_SERVICE";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion

                    #region Calling Vehicle Service & Filling Model

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SERVICE, "ZDMS_VEHICLE servisi çağırılıyor");
                    var rValue = pssc.ZDMS_VEHICLE(UserName, Password, null, null,
                                    lastSuccessCallDate,
                                    lastSuccessCallTime);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SERVICE, "ZDMS_VEHICLE servisi bitti");

                    XmlNodeList xmlList = rValue.SelectNodes("//tbl");
                    var listModel = (from XmlNode xmlNode in xmlList
                                     select new VehicleXMLModel
                                     {
                                         CodeSSID = xmlNode["CODE_SSID"].InnerText,
                                         VinNo = xmlNode["VIN_NO"].InnerText,
                                         EngineNo = xmlNode["ENGINE_NO"].InnerText,
                                         ModelYear = xmlNode["MODEL_YEAR"].InnerText,
                                         Color = xmlNode["COLOR"].InnerText,
                                         FactProdDate = xmlNode["FACT_PROD_DATE"].InnerText,
                                         FactQcntrlDate = xmlNode["FACT_QCNTRL_DATE"].InnerText,
                                         FactShipDate = xmlNode["FACT_SHIP_DATE"].InnerText,
                                         VatExclude = xmlNode["VAT_EXCLUDE"].InnerText,
                                         CustomerNo = xmlNode["KUNAG"].InnerText,
                                         CustomerName = xmlNode["NAME1"].InnerText,
                                         TCNo = (xmlNode["COUNTRY"].InnerText == "TR" && xmlNode["STCD2"].InnerText.Length == 11) ? xmlNode["STCD2"].InnerText : string.Empty,
                                         VatNo = string.IsNullOrEmpty((xmlNode["COUNTRY"].InnerText == "TR" && xmlNode["STCD2"].InnerText.Length == 11) ? xmlNode["STCD2"].InnerText : string.Empty) ? xmlNode["STCD2"].InnerText : string.Empty,
                                         VatOffice = xmlNode["STCD1"].InnerText,
                                         Address = xmlNode["ADRES"].InnerText,
                                         CountryShortCode = xmlNode["COUNTRY"].InnerText,
                                         PlateCode = xmlNode["REGION"].InnerText,
                                         City = xmlNode["BEZEI"].InnerText,
                                         Phone = xmlNode["TEL_NUMBER"].InnerText,
                                         MobilePhone = xmlNode["MOB_NUMBER"].InnerText,
                                         Email = xmlNode["SMTP_ADDR"].InnerText,
                                         FaxNo = xmlNode["FAX_NUMBER"].InnerText,
                                         Kamu = xmlNode["KAMU"].InnerText,
                                         BSTKD = xmlNode["BSTKD"].InnerText,
                                         PriceList = xmlNode["LISTEKODU"].InnerText
                                     }).Where(f => !string.IsNullOrEmpty(f.VinNo)).ToList();
                    #endregion

                    #region Db update

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SERVICE, "XMLtoDBVehicle metodu çağırılıyor");

                    var re = mainBL.XMLtoDBVehicle(listModel);
                    serviceCallLog.IsSuccess = re.IsSuccess;
                    serviceCallLog.ErrorMessage = re.ErrorMessage;
                    serviceCallLog.ErrorModel = re.ErrorModel;

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SERVICE, "XMLtoDBVehicle metodu bitti");

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

                if (!serviceCallLog.IsSuccess && serviceCallLog.ErrorModel != null && serviceCallLog.ErrorModel.Any())
                {
                    SendErrorVehiclesMail(serviceCallLog.ErrorModel);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SERVICE, string.Format("Hata : {0}", ex.Message));
                serviceCallLog.ErrorMessage = ex.Message;
                base.LogResponseService(serviceCallLog);
            }
            finally
            {
                base.EndService(serviceCallSchedule);
            }
            return Task.CompletedTask;
        }

        private void SendErrorVehiclesMail(IEnumerable<ServiceCallScheduleErrorListModel> errors)
        {
            string title = string.Format("ARAÇ SERVİSİ HATALARI - {0}", DateTime.Now.ToString("dd.MM.yyyy hh:mm"));
            new GosErrorEmailSender().Send(errors, title);
        }
    }
}
