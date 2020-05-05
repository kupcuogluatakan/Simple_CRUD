using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.Guarantee;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SparePart;
using ODMSService.Helpers;
using Quartz;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class GuaranteeCampaignPartPriceJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("CAMPAING_PRICE_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var guaranteeBl = new GuaranteeBL();

            var lastSuccessCallDateTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd HH:mm:ss") : DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            var lastSuccessCallDate = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
            var lastSuccessCallTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("HH:mm:ss") : DateTime.Now.ToString("HH:mm:ss");

            try
            {
                using (var pssc = GetClient())
                {
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CAMPAING_PRICE_SERVICE, "GetGuaranteeCampaignSsidCsv metodu çağırılıyor");
                    var ssids = guaranteeBl.GetGuaranteeCampaignSsidCsv().Model;
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CAMPAING_PRICE_SERVICE, "GetGuaranteeCampaignSsidCsv metodu bitti");

                    #region Request XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.ReqResDic = new Dictionary<string, string>()
                        {
                            {"DateTime", lastSuccessCallDateTime},
                            {"SSIDs", ssids}
                        };
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "SPARE_PART_SERVICE";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion Request XML Logging

                    #region Calling Spare Part Service & Filling Model

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CAMPAING_PRICE_SERVICE, "ZDMS_GIF_PARCA_MALIYET metodu çağırılıyor");
                    DataSet rValue = pssc.ZDMS_GIF_PARCA_MALIYET(UserName, Password, ssids);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CAMPAING_PRICE_SERVICE, "ZDMS_GIF_PARCA_MALIYET metodu bitti");

                    DataTable dtMain = new DataTable();

                    dtMain = rValue.Tables[0]; //For main spare part data

                    //Set our main model from dataset
                    var listModel = dtMain.AsEnumerable().Select(rowMain => new GuaranteeCampaignPartPriceXmlModel
                    {
                        Ssid = rowMain.Field<string>("gif_kayitno"),
                        Price = rowMain.Field<decimal>("maliyet"),
                    }).ToList();

                    #endregion Calling Spare Part Service & Filling Model

                    //DB Update
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CAMPAING_PRICE_SERVICE, "UpdateCampaignPriceXml metodu çağırılıyor");

                    var re = guaranteeBl.UpdateCampaignPriceXml(dtMain);
                    serviceCallLog.IsSuccess = re.IsSuccess;

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CAMPAING_PRICE_SERVICE, "UpdateCampaignPriceXml metodu bitti");

                    #region Response & Error XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.LogType = CommonValues.LogType.Response;
                        serviceCallLog.LogXml = XElement.Parse(rValue.ToXml());
                        base.LogResponseService(serviceCallLog);
                    }

                    #endregion Response & Error XML Logging
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.CAMPAING_PRICE_SERVICE, string.Format("Hata : {0}", ex.Message));
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
