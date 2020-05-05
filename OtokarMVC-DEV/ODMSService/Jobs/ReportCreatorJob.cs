using System;
using System.Collections.Generic;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using ODMSModel.Reports;
using ODMSService.Report;
using System.Configuration;
using System.IO;
using ODMSCommon.Security;
using Quartz;
using System.Threading.Tasks;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class ReportCreatorJob : JobBase, IJob
    {

        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("REPORT_CREATE_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();

            try
            {
                var reportBl = new ReportCreateBL();
                var filter = new ReportCreateModel();
                filter.IsComplete = null;

                var lastSuccessCallDateTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd HH:mm:ss") : DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                var lastSuccessCallDate = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
                var lastSuccessCallTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("HH:mm:ss") : DateTime.Now.ToString("HH:mm:ss");

                int total;
                var list = reportBl.GetAllReportCreate(filter, out total).Data;

                if (list.Count > 0)
                {
                    #region Request XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.ReqResDic = new Dictionary<string, string>()
                        {
                            {"DateTime", lastSuccessCallDateTime}
                        };
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "REPORT_CREATE";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        serviceCallLog.IsSuccess = true;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, string.Format("{0} adet rapor oluşturulacak", list.Count));
                }

                string folder = ConfigurationManager.AppSettings["ExcelFolder"];
                string fileName = string.Empty;

                #region  Appsettings e klasör pathi verilmedi ise log ekle

                if (string.IsNullOrEmpty(folder))
                {
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, "Rapor oluşturulacak klasör yeri bulunamadı.appSettingse ExcelFolder keyi ekleyiniz.");

                    var responseLogModel = new ServiceCallLogModel() { IsSuccess = false, LogId = serviceCallLog.LogId };
                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        responseLogModel.LogType = CommonValues.LogType.Response;
                        responseLogModel.LogXml = null;
                        responseLogModel.ErrorMessage = "Rapor oluşturulacak klasör yeri bulunamadı.appSettingse ExcelFolder keyi ekleyiniz.";
                        responseLogModel.LogErrorDesc = "Rapor oluşturulacak klasör yeri bulunamadı.appSettingse ExcelFolder keyi ekleyiniz.";
                        base.LogResponseService(responseLogModel);
                    }
                }

                #endregion

                #region Klasör yok ise oluşturulsun

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                #endregion

                foreach (var item in list)
                {
                    try
                    {
                        #region Rapor kaydının başlama işareti atılır

                        var updateFilter = new ReportCreateModel();
                        updateFilter.CommandType = "U";
                        updateFilter.Id = item.Id;
                        updateFilter.IsComplete = false;
                        updateFilter.IsSuccess = true;
                        reportBl.DMLReportCreate(updateFilter);

                        #endregion

                        var report = new ReportFactory((ReportType)item.ReportType);

                        var dic = new Dictionary<dynamic, dynamic>();

                        if (!string.IsNullOrEmpty(item.ParametersString))
                        {
                            var parameters = item.ParametersString.Split(new string[] { "#!?!#" }, StringSplitOptions.None);
                            foreach (var param in parameters)
                            {
                                var name = param.Split(new string[] { "#=#" }, StringSplitOptions.None)[0];
                                var value = param.Split(new string[] { "#=#" }, StringSplitOptions.None)[1];
                                dic.Add(name, value);
                            }
                        }

                        var user = UserManager.GetUserInfo(item.CreateUserId, item.CreateUser, string.Empty, item.CreateUserDealerId, item.CreateUserLanguageCode);
                        var respone = report.CreateExcel(user, folder, out fileName, dic, item.Columns);

                        #region Rapor kaydının bitiş işareti atılır

                        var result = new ReportCreateModel();
                        result.CommandType = "U";
                        result.Id = item.Id;
                        result.IsComplete = true;
                        result.IsSuccess = respone.Item1;
                        result.ErrorMessage = respone.Item2;
                        result.IsComplete = true;
                        result.FilePath = fileName;
                        reportBl.DMLReportCreate(result);

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        #region Rapor kaydında hata işareti atılır

                        var result = new ReportCreateModel();
                        result.CommandType = "U";
                        result.Id = item.Id;
                        result.IsComplete = true;
                        result.IsSuccess = false;
                        result.ErrorMessage = ex.Message;
                        result.IsComplete = true;
                        result.FilePath = null;
                        reportBl.DMLReportCreate(result);

                        #endregion
                    }

                }

                if (list.Count > 0)
                {
                    #region  Response & Error XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.LogType = CommonValues.LogType.Response;
                        serviceCallLog.LogXml = null;
                        base.LogResponseService(serviceCallLog);
                    }

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, "Tamamlandı");

                    #endregion
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, string.Format("Hata : {0}", ex.Message));
            }
            finally
            {
                base.EndService(serviceCallSchedule);
            }

            return Task.CompletedTask;
        }

    }
}
