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

namespace ODMSService.ServiceClasses
{
    internal class FreePurchaseOrderService : ServiceBase
    {
        private ServiceScheduleModel serviceModel = new ServiceScheduleModel();
        public FreePurchaseOrderService(ServiceScheduleModel model)
        {
            serviceModel = model;
        }

        public override void Execute()
        {
            var serviceCallSchedule = base.StartService(serviceModel.Id);
            var serviceCallLog = new ServiceCallLogModel();
            var responseLogModel = new ServiceCallLogModel(serviceCallSchedule.TriggerServiceName);

            var lastSuccessCallDateTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd HH:mm:ss") : DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            var lastSuccessCallDate = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
            var lastSuccessCallTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("HH:mm:ss") : DateTime.Now.ToString("HH:mm:ss");


            #region Request XML Logging

            if (serviceCallSchedule.IsResponseLogged)
            {
                serviceCallLog.ReqResDic = new Dictionary<string, string>()
                    {
                        {"DateTime", lastSuccessCallTime}
                    };
                serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "FREE_PO_SERVUCE";
                serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                serviceCallLog.LogType = CommonValues.LogType.Request;
                base.LogRequestService(serviceCallLog);
                responseLogModel.LogId = serviceCallLog.LogId;
            }

            #endregion Request XML Logging

            using (var pssc = GetClient())
            {

                #region Calling Free Purchase Service & Filling Model
                //Model.LastSuccessCall = new DateTime(2018,12,27);
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "ZDMS_BEDELSIZ_SIPARIS servisi çağırılıyor");
                DataSet rValue = pssc.ZDMS_BEDELSIZ_SIPARIS(UserName, Password,
                    lastSuccessCallDate, lastSuccessCallTime, null,
                    null,
                    null);
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "ZDMS_BEDELSIZ_SIPARIS servisi bitti");

                DataTable dtMaster = rValue.Tables["Table1"]; // purchase order mst
                DataTable dtDetail = rValue.Tables["Table2"]; // purchase order det
                if (dtMaster != null && dtDetail != null)
                {
                    PurchaseOrderBL poBo = new PurchaseOrderBL();
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "SaveFreePurchaseOrder metodu çağırılıyor");
                    responseLogModel = poBo.SaveFreePurchaseOrder(UserManager.UserInfo, dtMaster, dtDetail);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "SaveFreePurchaseOrder metodu bitti");
                  
                    if (responseLogModel.IsSuccess)
                    {
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.FREE_PO_SERVICE, "SavePurchNoUpdate metodu çağırılıyor");
                        SavePurchNoUpdate(responseLogModel);
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

            base.EndService(serviceCallSchedule);

            if (!responseLogModel.IsSuccess)
            {
                base.SendErrorMail(responseLogModel);
            }

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