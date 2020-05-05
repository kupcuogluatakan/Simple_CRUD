﻿using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.VehicleType;
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
    internal class VehicleTypeJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("VEHICLE_TYPE_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var bl = new VehicleTypeBL();

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
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName;// "VEHICLE_TYPE_SERVICE";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_TYPE_SERVICE, "ZDMS_VEHICLE_TYPE servisi çağırılıyor");
                    var rValue = pssc.ZDMS_VEHICLE_TYPE(UserName, Password, null);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_TYPE_SERVICE, "ZDMS_VEHICLE_TYPE servisi bitti");

                    XmlNodeList xmlList = rValue.SelectNodes("//tbl");
                    var listModel = (from XmlNode xmlNode in xmlList
                                     select new VehicleTypeXMLViewModel
                                     {
                                         TypeSSID = xmlNode["TYPE_SSID"].InnerText,
                                         ModelSSID = xmlNode["MODEL_SSID"].InnerText,
                                         TypeName = xmlNode["TYPE_NAME"].InnerText,
                                         CommandType = CommonValues.DMLType.Insert
                                     }).ToList();

                    #region Db update

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_TYPE_SERVICE, "XMLtoDBVehicleType metodu çağırılıyor");

                    var re = bl.XMLtoDBVehicleType(listModel);
                    serviceCallLog.IsSuccess = re.IsSuccess;
                    serviceCallLog.ErrorMessage = re.Message;

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_TYPE_SERVICE, "XMLtoDBVehicleType metodu bitti");

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
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_TYPE_SERVICE, string.Format("Hata : {0}", ex.Message));
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