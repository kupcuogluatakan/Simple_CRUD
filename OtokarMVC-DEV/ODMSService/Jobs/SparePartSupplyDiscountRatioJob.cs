using ODMSBusiness;
using ODMSCommon;
using ODMSModel.PurchaseOrder;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ODMSCommon.Security;
using Quartz;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class SparePartSupplyDiscountRatioJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("ZDMS_YP_ISKONTO").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();

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
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "ZDMS_YP_ISKONTO";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion Request XML Logging

                    #region Calling Free Purchase Service & Filling Model

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.ZDMS_YP_ISKONTO, "ZDMS_YP_ISKONTO servisi çağırılıyor");

                    DataSet rValue = pssc.ZDMS_YP_ISKONTO(UserName, Password, lastSuccessCallDate, lastSuccessCallTime);

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.ZDMS_YP_ISKONTO, "ZDMS_YP_ISKONTO servisi bitti");

                    DataTable dtMaster = rValue.Tables["Table1"];
                    if (dtMaster != null)
                    {
                        SparePartBL poBo = new SparePartBL();
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.ZDMS_YP_ISKONTO, "SaveSparePartSupplyDicsountRatio metodu çağırılıyor");

                        var re = poBo.SaveSparePartSupplyDicsountRatio(UserManager.UserInfo, dtMaster);
                        serviceCallLog.IsSuccess = re.IsSuccess;
                        serviceCallLog.ErrorMessage = re.ErrorMessage;
                        serviceCallLog.ErrorModel = re.ErrorModel;

                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.ZDMS_YP_ISKONTO, "SaveSparePartSupplyDicsountRatio metodu bitti");

                        if (serviceCallLog.IsSuccess)
                        {
                            SavePurchNoUpdate(serviceCallLog);
                        }
                    }

                    #endregion Calling Free Purchase Service & Filling Model

                    #region Response & Error XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.LogType = CommonValues.LogType.Response;
                        serviceCallLog.LogXml = XElement.Parse(rValue.ToXml());
                        base.LogResponseService(serviceCallLog);
                    }

                    #endregion Response & Error XML Logging
                }

                if (!serviceCallLog.IsSuccess)
                {
                    base.SendErrorMail(serviceCallLog);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.ZDMS_YP_ISKONTO, string.Format("Hata : {0}", ex.Message));
                serviceCallLog.ErrorMessage = ex.Message;
                base.LogResponseService(serviceCallLog);
            }
            finally
            {
                base.EndService(serviceCallSchedule);
            }

            return Task.CompletedTask;
        }

        private void SavePurchNoUpdate(ServiceCallLogModel logModel)
        {
            using (var pssc = GetClient())
            {
                List<string> poList = new List<string>();
                PurchaseOrderBL poBo = new PurchaseOrderBL();
                List<PurchaseOrderListModel> list = poBo.ReturnIsSASNoNotSentPurchaseOrders(UserManager.UserInfo).Data;
                if (list.Count > 0)
                {
                    foreach (PurchaseOrderListModel listModel in list)
                    {
                        string poNumber = listModel.PoNumber.GetValue<string>();
                        string sapOfferNo = poBo.GetPurchaseOrderSAPOfferNo(UserManager.UserInfo, poNumber).Model;
                        if (!String.IsNullOrEmpty(sapOfferNo))
                        {
                            var rValue = pssc.ZDMS_PURCH_NO_UPDATE(UserName, Password, sapOfferNo, poNumber);
                            DataTable dt = rValue.Tables["Table1"];
                            if (dt != null && dt.Rows.Count != 0)
                            {
                                var control = (from DataRow r in dt.Rows
                                               where r["TYPE"].GetValue<string>() != "S"
                                               select r);
                                if (!control.Any())
                                {
                                    poList.Add(poNumber);
                                }
                            }
                        }
                    }
                    poBo.UpdatePurchaseOrderIsSASNoSentValue(UserManager.UserInfo, poList, logModel);
                }
            }
        }
    }
}