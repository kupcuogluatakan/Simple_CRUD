using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.StockCardPriceListModel;
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
using System.Configuration;
using System.IO;
using Quartz;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class RetailPriceJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("RETAIL_PRICE_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, "RetailPriceJob başladı");

            var serviceCallLog = new ServiceCallLogModel();
            var mainBl = new SparePartBL();

            var lastSuccessCallDateTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd HH:mm:ss") : DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            var lastSuccessCallDate = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
            var lastSuccessCallTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("HH:mm:ss") : DateTime.Now.ToString("HH:mm:ss");

            List<string> ssidPriceList = mainBl.ListPriceListSSIDs().Data;

            try
            {
                #region Askeri araçlar bu listede olmamalı

                string janpolPriceListId = CommonBL.GetGeneralParameterValue("JANPOL_PRICE_LIST_ID").Model;
                var janPolPriceListSsid = mainBl.GetPriceListSsidByPriceListId(int.Parse(janpolPriceListId)).Model;
                if (!string.IsNullOrEmpty(janPolPriceListSsid) && ssidPriceList.Contains(janPolPriceListSsid))
                {
                    ssidPriceList.Remove(janPolPriceListSsid);
                }

                #endregion

                #region Tüm fiyat listesini dön

                foreach (var ssid in ssidPriceList)
                {
                    serviceCallLog = new ServiceCallLogModel();

                    try
                    {
                        using (var pssc = GetClient())
                        {
                            #region Request XML Logging

                            if (serviceCallSchedule.IsResponseLogged)
                            {
                                serviceCallLog.ReqResDic = new Dictionary<string, string>()
                                {
                                    {"DateTime", lastSuccessCallDateTime},
                                    {"PriceListSSID", ssid}
                                };
                                serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "RETAIL_PRICE_SERVICE";
                                serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                                serviceCallLog.LogType = CommonValues.LogType.Request;
                                serviceCallLog.IsSuccess = true;
                                base.LogRequestService(serviceCallLog);
                            }

                            #endregion Request XML Logging

                            var listModel = new List<SparePartPriceXMLModel>();

                            #region Calling Spare Part Price Service & Filling Model

                            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, string.Format("{0} için ZDMS_PARAKENDE_FIYAT_LISTESI servisi çağırılıyor", ssid));

                            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, string.Format("{0} için web servis {1} {2} tarihi ile çağırılacak ", ssid, lastSuccessCallDate, lastSuccessCallTime));

                            DataSet rValue = pssc.ZDMS_PARAKENDE_FIYAT_LISTESI(UserName, Password, lastSuccessCallDate, lastSuccessCallTime, ssid);

                            if (rValue.Tables.Count == 0)
                                continue;

                            //Type of response is datatable for spare part price list
                            DataTable dtMain = new DataTable();
                            dtMain = rValue.Tables["Table1"]; //For main spare part price list

                            if (dtMain.Rows.Count > 0)
                            {
                                dtMain = dtMain.AsEnumerable().Union(dtMain.AsEnumerable()).CopyToDataTable();

                                #region Fiyat listesi büyük ise parça parça olarak alınır

                                if (rValue.Tables["Table2"].Rows[0]["C_PARTIAL"].ToString() == "X")
                                {
                                    bool result = true;
                                    while (result)
                                    {
                                        rValue = pssc.ZDMS_PARAKENDE_FIYAT_LISTESI_P(UserName, Password);
                                        var dtMain1 = rValue.Tables["Table1"];
                                        if (dtMain1.Rows.Count > 0)
                                        {
                                            dtMain = dtMain.AsEnumerable().Union(dtMain1.AsEnumerable()).CopyToDataTable();
                                        }

                                        if (rValue.Tables["Table2"].Rows[0]["C_PARTIAL"].ToString() != "X" || dtMain1.Rows.Count == 0)
                                            result = false;
                                    }
                                }

                                #endregion
                            }

                            #endregion Calling Spare Part Price Service & Filling Model

                            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, string.Format("{0} için ZDMS_PARAKENDE_FIYAT_LISTESI servisi bitti", ssid));

                            if (dtMain.Rows.Count > 0)
                            {
                                #region CSV Log  

                                var lines = new List<string>();
                                string[] columnNames = dtMain.Columns.Cast<DataColumn>().
                                                                  Select(column => column.ColumnName).
                                                                  ToArray();

                                var header = string.Join(",", columnNames);
                                lines.Add(header);

                                var valueLines = dtMain.AsEnumerable()
                                                   .Select(row => string.Join(",", row.ItemArray));
                                lines.AddRange(valueLines);
                                var folder = string.Format("{0}excel", AppDomain.CurrentDomain.BaseDirectory);
                                if (!Directory.Exists(folder))
                                {
                                    Directory.CreateDirectory(folder);
                                }
                                var filepath = string.Format("{0}excel\\{1}_{2}.csv", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd_HHmm"), ssid);
                                File.WriteAllLines(filepath, lines);

                                #endregion

                                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, string.Format("{0} için XMLtoDBSparePartPrice metodu çağırılıyor", ssid));
                                //DB Update
                                var re = mainBl.XMLtoDBSparePartPrice(dtMain).Model;
                                serviceCallLog.IsSuccess = re.IsSuccess;
                                serviceCallLog.ErrorMessage = re.ErrorMessage;
                                serviceCallLog.ErrorModel = re.ErrorModel;

                                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, string.Format("{0} için XMLtoDBSparePartPrice metodu tamamlandı", ssid));
                                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, Environment.NewLine);

                            }



                            #region Response & Error XML Logging

                            if (serviceCallSchedule.IsResponseLogged)
                            {
                                serviceCallLog.LogType = CommonValues.LogType.Response;
                                serviceCallLog.LogXml = XElement.Parse(rValue.ToXml());
                                base.LogResponseService(serviceCallLog);
                            }

                            rValue.Dispose();

                            #endregion Response & Error XML Logging
                        }
                    }
                    catch (Exception ex)
                    {
                        #region Response & Error XML Logging

                        if (serviceCallSchedule.IsResponseLogged)
                        {
                            serviceCallLog.ErrorMessage = ex.Message;
                            serviceCallLog.LogType = CommonValues.LogType.Response;
                            serviceCallLog.LogXml = new XElement("DataSet", ex.Message);
                            base.LogResponseService(serviceCallLog);
                        }

                        #endregion

                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, string.Format("{0} için Hata : {1}", ssid, ex.Message));
                    }
                    finally
                    {
                        base.SendErrorMail(serviceCallLog);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, string.Format("Hata : {0}", ex.Message));
            }
            finally
            {
                base.EndService(serviceCallSchedule);
            }

            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RETAIL_PRICE_SERVICE, "RetailPriceJob bitti");

            return Task.CompletedTask;
        }
    }
}