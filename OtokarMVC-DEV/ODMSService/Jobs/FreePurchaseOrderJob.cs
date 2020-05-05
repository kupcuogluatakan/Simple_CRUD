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
using System.IO;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class FreePurchaseOrderJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("FREE_PO_SERVICE").Model;
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
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "FREE_PO_SERVUCE";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion Request XML Logging

                    #region Calling Free Purchase Service & Filling Model

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "ZDMS_BEDELSIZ_SIPARIS servisi çağırılıyor");
                    DataSet rValue = pssc.ZDMS_BEDELSIZ_SIPARIS(UserName, Password,
                        lastSuccessCallDate, null, null, null, null);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "ZDMS_BEDELSIZ_SIPARIS servisi bitti");

                    DataTable dtMaster = rValue.Tables["Table1"]; // purchase order mst
                    DataTable dtDetail = rValue.Tables["Table2"]; // purchase order det
                    if (dtMaster != null && dtDetail != null)
                    {
                        #region CSV Log  

                        var lines = new List<string>();
                        string[] columnNames = dtMaster.Columns.Cast<DataColumn>().
                                                          Select(column => column.ColumnName).
                                                          ToArray();

                        var header = string.Join(",", columnNames);
                        lines.Add(header);

                        var valueLines = dtMaster.AsEnumerable()
                                           .Select(row => string.Join(",", row.ItemArray));
                        lines.AddRange(valueLines);
                        var folder = string.Format("{0}excel", AppDomain.CurrentDomain.BaseDirectory);
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        var filepath = string.Format("{0}excel\\freePOList_{1}.csv", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd_HHmm"));
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "filepath-->" + filepath);
                        File.WriteAllLines(filepath, lines);

                        #endregion



                        PurchaseOrderBL poBo = new PurchaseOrderBL();
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "SaveFreePurchaseOrder metodu çağırılıyor");

                        var re = poBo.SaveFreePurchaseOrder(UserManager.UserInfo, dtMaster, dtDetail);
                        serviceCallLog.IsSuccess = re.IsSuccess;
                        serviceCallLog.ErrorMessage = re.ErrorMessage;
                        serviceCallLog.ErrorModel = re.ErrorModel;

                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "SaveFreePurchaseOrder metodu bitti");

                        if (serviceCallLog.IsSuccess)
                        {
                            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "SavePurchNoUpdate metodu çağırılıyor");
                            SavePurchNoUpdate(serviceCallLog);
                            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "SavePurchNoUpdate metodu bitti");
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
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, string.Format("Hata : {0}", ex.Message));

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
                list = list.Where(e => !string.IsNullOrEmpty(e.SapOfferNo)).ToList();
                if (list.Count > 0)
                {
                    foreach (PurchaseOrderListModel listModel in list)
                    {
                        var rValue = pssc.ZDMS_PURCH_NO_UPDATE(UserName, Password, listModel.SapOfferNo, listModel.PoNumber.ToString());
                        DataTable dt = rValue.Tables["Table1"];
                        if (dt != null && dt.Rows.Count != 0)
                        {
                            var control = (from DataRow r in dt.Rows
                                           where r["TYPE"].GetValue<string>() != "S"
                                           select r);
                            if (!control.Any())
                            {
                                poList.Add(listModel.PoNumber.ToString());
                            }
                        }
                    }
                    poBo.UpdatePurchaseOrderIsSASNoSentValue(UserManager.UserInfo, poList, logModel);
                }
            }
        }
    }
}