using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SparePart;
using ODMSService.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ODMSService;
using Quartz;
using System.IO;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class SparePartJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("SPARE_PART_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var bl = new SparePartBL();

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
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "SPARE_PART_SERVICE";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion Request XML Logging

                    #region Calling Spare Part Service & Filling Model

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SERVICE, "ZDMS_MLZ_AV_TRANSFER servisi çağırılıyor");
                    //Calling Spare Part Service
                    DataSet rValue = pssc.ZDMS_MLZ_AV_TRANSFER(UserName, Password,
                                    lastSuccessCallDate,
                                    lastSuccessCallTime);

                    //Type of response is datatable for spare part
                    DataTable dtMain = new DataTable();
                    DataTable dtLang = new DataTable();

                    dtMain = rValue.Tables["Table1"]; //For main spare part data
                    dtLang = rValue.Tables["Table2"]; //For spare parts lang data
                                                      //Set our main model from dataset


                    if (dtMain.Rows.Count > 0)
                    {
                        dtMain = dtMain.AsEnumerable().Union(dtMain.AsEnumerable()).CopyToDataTable();

                        #region Parça listesi büyük ise parça parça olarak alınır

                        if (rValue.Tables["Table3"].Rows[0]["C_PARTIAL"].ToString() == "X")
                        {
                            bool result = true;
                            while (result)
                            {
                                rValue = pssc.ZDMS_MLZ_AV_TRANSFER_P(UserName, Password);
                                var dtMain1 = rValue.Tables["Table1"];
                                if (dtMain1.Rows.Count > 0)
                                {
                                    dtMain = dtMain.AsEnumerable().Union(dtMain1.AsEnumerable()).CopyToDataTable();
                                }
                                var dtLang1 = rValue.Tables["Table2"];
                                if (dtLang1.Rows.Count > 0)
                                {
                                    dtLang = dtLang.AsEnumerable().Union(dtLang1.AsEnumerable()).CopyToDataTable();
                                }

                                if (rValue.Tables["Table3"].Rows[0]["C_PARTIAL"].ToString() != "X" || dtMain1.Rows.Count == 0)
                                    result = false;
                            }
                        }

                        #endregion
                    }


                    #endregion Calling Spare Part Service & Filling Model

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SERVICE, "ZDMS_MLZ_AV_TRANSFER servisi bitti");

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
                        var filepath = string.Format("{0}excel\\partList_{1}.csv", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd_HHmm"));
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SERVICE, "filepath-->" + filepath);
                        File.WriteAllLines(filepath, lines);

                        #endregion
                        
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SERVICE, "XMLtoDBSparePart methodu çağırılıyor");
                        //DB Update
                        var re = bl.XMLtoDBSparePart(dtMain, dtLang);
                        serviceCallLog.IsSuccess = re.IsSuccess;
                        serviceCallLog.ErrorMessage = re.ErrorMessage;
                        serviceCallLog.ErrorModel = re.ErrorModel;

                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SERVICE, "XMLtoDBSparePart methodu bitti");
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SERVICE, Environment.NewLine);
                    }

                    #region Response & Error XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.LogType = CommonValues.LogType.Response;
                        serviceCallLog.LogXml = XElement.Parse(rValue.ToXml());
                        base.LogResponseService(serviceCallLog);
                    }

                    #endregion Response & Error XML Logging
                }

                if (!serviceCallLog.IsSuccess && serviceCallLog.ErrorModel != null && serviceCallLog.ErrorModel.Any())
                {
                    SendErrorMail(serviceCallLog.ErrorModel);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SERVICE, string.Format("Hata : {0}", ex.Message));

                serviceCallLog.ErrorMessage = ex.Message;
                base.LogResponseService(serviceCallLog);
            }
            finally
            {
                base.EndService(serviceCallSchedule);
            }
            return Task.CompletedTask;
        }

        private void SendErrorMail(IEnumerable<ServiceCallScheduleErrorListModel> errors)
        {
            string title = string.Format("PARÇA AKTARIM SERVİSİ HATALARI - {0}", DateTime.Now.ToString("dd.MM.yyyy hh:mm"));
            new GosErrorEmailSender().Send(errors, title);
        }

    }
}