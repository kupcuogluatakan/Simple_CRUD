using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using ODMSCommon.Security;
using Quartz;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class GetGifNoJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("GIF_SAVE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var bl = new GuaranteeBL();
            var listModel = bl.GetGuarantee(UserManager.UserInfo).Data;

            var lastSuccessCallDateTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd HH:mm:ss") : DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            var lastSuccessCallDate = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
            var lastSuccessCallTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("HH:mm:ss") : DateTime.Now.ToString("HH:mm:ss");

            try
            {
                using (var pssc = GetClient())
                {
                    foreach (var model in listModel)
                    {
                        try
                        {
                            serviceCallLog = new ServiceCallLogModel();

                            #region Request XML Logging

                            if (serviceCallSchedule.IsResponseLogged)
                            {
                                serviceCallLog.ReqResDic = new Dictionary<string, string>()
                        {
                            {"DateTime", lastSuccessCallTime},
                            {"DealerSSID", model.DealerSSId},
                            {"VINNO", model.VehicleVinNo},
                            {"SSIdGuarantee", model.SSIdGuarantee},
                            {"RequestUser", model.RequestUser},
                            {"OnayRefNo", ""},
                            {"FailureCode", model.FauilureCode},
                            {"BreakDownDate", model.BreakdownDate.GetValue<DateTime>().ToString("yyyyMMdd")},
                            {"LabourTotalPrice", model.LabourTotalPrice},
                            {"PartsTotalPrice", model.PartsTotalPrice},
                            {"CurrencyCode", model.CurrencyCode},
                            {"WorkOrderId", model.WorkOrderId},
                            {"VehicleEnteryDate", model.VehicleEnteryDate.GetValue<DateTime>().ToString("yyyyMMdd")},
                            {"VehicleLeaveDate", model.VehicleLeaveDate.GetValue<DateTime>().ToString("yyyyMMdd")},
                            {"CategoryLookval", model.CategoryLookval},
                            {"RequestDesc", model.RequestDesc},
                            {"RequestDesc2", model.RequestDesc2},
                            {"VehicleKm", model.VehicleKm},
                            {"PdiGif", model.PdiGif},
                            {"CampaignCode", model.CampaignCode},
                            {"CustomerName", model.CustomerName},
                            {"CustomerAddress", model.CustomerAddress},
                            {"CustomerPhone", model.CustomerPhone},
                            {"CustomerMobile", model.CustomerMobile},
                            {"CustomerFax", model.CustomerFax},
                            {"VehiclePlate", model.VehiclePlate},
                            {"VehicleNotes", model.VehicleNotes},
                            {"ApproveUser", model.ApproveUser},
                            {"PartsColumn", model.PartsColumn},
                            {"LaboursColumn", model.LaboursColumn},
                            { "ExternalLaboursColumn", model.ExternalLaboursColumn },
                            { "IndicatorTypeName", model.IndicatorTypeName },
                            { "ProcessTypeName", model.ProcessTypeName }
                        };
                                serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName;
                                serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                                serviceCallLog.LogType = CommonValues.LogType.Request;
                                base.LogRequestService(serviceCallLog);
                            }

                            #endregion

                            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GIF_SAVE, "ZDMS_GIF_KAYIT servisi çağırılıyor");

                            #region SAP ZDMS_GIF_KAYIT servisi

                            var rValue = pssc.ZDMS_GIF_KAYIT(UserName, Password,
                                model.DealerSSId,
                                model.VehicleVinNo,
                                model.RequestUser,
                                "",
                                model.FauilureCode,
                                model.BreakdownDate == null ? "" : model.BreakdownDate.GetValue<DateTime>().ToString("yyyyMMdd"),
                                model.LabourTotalPrice,
                                model.PartsTotalPrice,
                                model.CurrencyCode,
                                model.WorkOrderId,
                                model.VehicleEnteryDate == null ? "" : model.VehicleEnteryDate.GetValue<DateTime>().ToString("yyyyMMdd"),
                                model.VehicleLeaveDate == null ? "" : model.VehicleLeaveDate.GetValue<DateTime>().ToString("yyyyMMdd"),
                                model.CategoryLookval,
                                model.RequestDesc,
                                model.RequestDesc2,
                                model.VehicleKm,
                                model.PdiGif,
                                model.CampaignCode,
                                model.CustomerName,
                                model.CustomerAddress,
                                model.CustomerPhone,
                                model.CustomerMobile,
                                model.CustomerFax,
                                model.VehiclePlate,
                                model.VehicleNotes,
                                model.ApproveUser,
                                model.PartsColumn,
                                model.LaboursColumn,
                                model.ExternalLaboursColumn,
                                model.IndicatorTypeName,
                                model.ProcessTypeName);

                            #endregion

                            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GIF_SAVE, "ZDMS_GIF_KAYIT servisi bitti");

                            #region Db işlemi

                            if (rValue.SelectSingleNode("RESULT/gif_kayit_no") != null)
                            {
                                Int64 gifNo = Convert.ToInt64(rValue.SelectSingleNode("RESULT/gif_kayit_no").InnerText);
                                if (gifNo > 0)
                                {

                                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GIF_SAVE, string.Format("SetSuccessResultGif metodu çağırılıyor GuaranteeId : {0} - GuaranteeSeq : {1}", model.GuaranteeId, model.GuaranteeSeq));
                                    //If returns gif and its > 0 then update GUARANTEE_SSID 
                                    var re = bl.SetSuccessResultGif(model.GuaranteeId, model.GuaranteeSeq, gifNo);
                                    serviceCallLog.IsSuccess = re.IsSuccess;
                                    serviceCallLog.ErrorMessage = re.Message;

                                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GIF_SAVE, string.Format("SetSuccessResultGif metodu çağırılıyor GuaranteeId : {0} - GuaranteeSeq : {1}", model.GuaranteeId, model.GuaranteeSeq));
                                    serviceCallLog.IsSuccess = true;
                                }
                                else
                                {
                                    serviceCallLog.IsSuccess = false;
                                }
                            }
                            else
                            {
                                serviceCallLog.IsSuccess = false;
                            }

                            #endregion

                            #region Response & Error XML Logging

                            if (serviceCallSchedule.IsResponseLogged)
                            {
                                serviceCallLog.LogType = CommonValues.LogType.Response;
                                serviceCallLog.LogXml = new XElement(XDocument.Parse(rValue.OuterXml).Root);
                                base.LogResponseService(serviceCallLog);
                            }

                            #endregion

                            if (!serviceCallLog.IsSuccess)
                                bl.SendFailMailDb(model.GuaranteeId, model.GuaranteeSeq, model.WorkOrderId, serviceCallLog.LogId, rValue.InnerText);
                        }
                        catch (Exception ex)
                        {
                            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GIF_SAVE, string.Format("Hata : {0}", ex.Message));
                            serviceCallLog.ErrorMessage = ex.Message;
                            base.LogResponseService(serviceCallLog);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GIF_SAVE, string.Format("Hata : {0}", ex.Message));
            }
            finally
            {
                base.EndService(serviceCallSchedule);
            }
            return Task.CompletedTask;
        }

        private string SetSapCustomerDigit(string ssid, int sapDigit)
        {
            if (!string.IsNullOrEmpty(ssid))
            {
                int digit = ssid.Length; //6 
                for (int I = 0; I < sapDigit - digit; I++)
                {
                    ssid = "0" + ssid;
                }
            }
            return ssid;
        }
    }
}
