using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SparePart;
using ODMSService.Helpers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class SparePartSplittingJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("SPARE_PART_SPLITTING").Model;
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
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName;
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SPLITTING, "ZDMS_PRODUCT_SELECTION servisi çağırılıyor");
                    DataSet rValue = pssc.ZDMS_PRODUCT_SELECTION(UserName, Password);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SPLITTING, "ZDMS_PRODUCT_SELECTION servisi bitti");
                    DataTable dtMain = rValue.Tables["Table1"];
                    var listModel =
                        dtMain.AsEnumerable().Select(rowMain => new SparePartSplitterXMLModel
                        {
                            CounterNo = rowMain.Field<string>("STPOZ"),
                            CreateDate = rowMain.Field<string>("DATUM"),
                            CreateUser = rowMain.Field<string>("ERNAM"),
                            GroupId = rowMain.Field<string>("USTGR"),
                            NewPartCode = rowMain.Field<string>("MATNR"),
                            OldPartCode = rowMain.Field<string>("MATWA"),
                            Quantity = rowMain.Field<string>("MENGE"),
                            Usable = rowMain.Field<string>("PSDSP"),
                            RankNo = rowMain.Field<string>("SEQNO")
                        }).ToList();

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SPLITTING, "XMLtoDBSparePartSplit metodu çağırılıyor");

                    var re = bl.XMLtoDBSparePartSplit(listModel);
                    serviceCallLog.IsSuccess = re.IsSuccess;
                    serviceCallLog.ErrorMessage = re.Message;

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SPLITTING, "XMLtoDBSparePartSplit metodu bitti");

                    #region Response & Error XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.LogType = CommonValues.LogType.Response;
                        serviceCallLog.LogXml = XElement.Parse(rValue.ToXml());
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
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.SPARE_PART_SPLITTING, string.Format("Hata : {0}", ex.Message));
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