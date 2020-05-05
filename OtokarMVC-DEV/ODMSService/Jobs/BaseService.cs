using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ODMSService.ServiceClasses
{
    internal abstract class BaseService
    {
        public ServiceScheduleModel Model { get; private set; }

        protected readonly string UserName;
        protected readonly string Password;
        private readonly string _serviceType;
        private int _threadWaitTime;
        private const int DefaultWaitSeconds = 60;
        private long _logId = 0;

        internal readonly LoggingHelper Log = new LoggingHelper("ODMSApplication", "ODMSServiceLog");
        internal readonly ServiceCallScheduleBL ServiceCall = new ServiceCallScheduleBL();

        private static readonly object _sync = new object();
        internal static bool Run = true;
        private bool _isSuccess = false;
        private bool _shouldSetLastSuccessCallDate = true;

        protected BaseService(ServiceScheduleModel model)
        {
            Model = model;
            UserName = ConfigurationManager.AppSettings.Get("PSSCUser");
            Password = ConfigurationManager.AppSettings.Get("PSSCPass");
            _serviceType = ConfigurationManager.AppSettings.Get("ServiceReferance");
            SetWaitTime();
        }

        public long LogId
        {
            get { return _logId; }
            set
            {
                lock (_sync)
                {
                    _logId = value;
                }
            }
        }

        private void SetWaitTime()
        {
            int waitSecondVal = 0;

            if (int.TryParse(CommonBL.GetGeneralParameterValue("WINSERVICE_WAIT_TIME").Model, out waitSecondVal) == false)
            {
                waitSecondVal = DefaultWaitSeconds;
            }
            _threadWaitTime = waitSecondVal * 1000;//in miliseconds
        }

        private bool CheckIfActive(ServiceScheduleModel mainModel)
        {
            if (mainModel != null)
            {
                return mainModel.IsActive;
            }
            return false;
        }

        protected void DontLastSuccessCallDate()
        {
            _shouldSetLastSuccessCallDate = false;
            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("DontLastSuccessCallDate =>", _shouldSetLastSuccessCallDate));
        }

        protected bool CheckServiceTime(ServiceScheduleModel mainModel)
        {
            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("IS TRIGGER START =>", mainModel.IsTrigger));
            _isSuccess = false;
            if (CheckIfActive(mainModel))
            {
                bool isServiceCall = mainModel.IsTrigger;

                if (isServiceCall) return true;

                if (mainModel.NextCallKS <= DateTime.Now)
                {
                    isServiceCall = true;
                }
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("IS TRIGGER END is service call=>", isServiceCall));
                return isServiceCall;
            }
            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("IS TRIGGER END =>", false));
            return false;
        }

        private void SetLastCallTimes(ServiceScheduleModel model, DateTime dt)
        {
            model.LastCallKS = dt;
            model.LastSuccessCall = model.LastSuccessCall;//success ? dt : model.LastSuccessCall;
        }

        protected dynamic GetClient()
        {
            try
            {
                if (ConfigurationManager.AppSettings.GetValues("ServiceReferance")[0] == "Test")
                    return new OtokarService.ProjectServiceSoapClient();
                return new tr.com.otokar.service.ProjectServiceSoapClient();
            }
            catch (Exception ex)
            {
                if (ConfigurationManager.AppSettings.GetValues("ServiceReferance")[0] == "Test")
                    return new OtokarService.ProjectServiceSoapClient("ProjectServiceSoap1");
                return new tr.com.otokar.service.ProjectServiceSoapClient("ProjectServiceSoap");
            }
        }

        public static void OnStop()
        {
            lock (_sync)
            {
                Run = false;
            }
        }

        public static void OnStart()
        {
            lock (_sync)
            {
                LoggingHelper log = new LoggingHelper("ODMSApplication", "ODMSServiceLog");
                log.WriteToEventLog("Base Started", "info");
                Run = true;
            }
        }

        private void WaitForNextCall()
        {
            ServiceCall.RefreshCallSchedule(Model);
            Thread.Sleep(_threadWaitTime);
        }

        protected void LogException(Exception ex, long logId = 0)
        {
            Log.WriteToEventLog(
                string.Format("ErrorMessage:{0} TargetSite:{1} InnerException:{2}", ex.Message, ex.TargetSite,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty), "ERROR");
            if (Model.IsLogged)
            {
                var logModel = new ServiceCallLogModel();
                logModel.LogType = CommonValues.LogType.Response;
                logModel.LogErrorDesc = ex.Message;
                logModel.LogId = LogId;
                LogServiceCall(logModel);
                _logId = 0;
            }
        }

        private void LogServiceCall(ServiceCallLogModel callLogModel)
        {
            ServiceCall.LogResponseService(callLogModel);
        }

        private void UpdateModel()
        {
            var data = ServiceCall.Get(UserManager.UserInfo, new ServiceCallScheduleViewModel() { ServiceId = Model.Id }).Model;
            try
            {
                Model.IsActive = data.IsActive;
                Model.IsLogged = data.IsResponseLogged;
                Model.IsTrigger = data.IsTriggerService;
                Model.LastCallKS = DateTime.Parse(data.LastCallDate, new CultureInfo(UserManager.LanguageCode));
                Model.NextCallKS = data.NextCallDate;
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, string.Format("UpdateModel de '{0}' tarihi çevrilemedi", data.LastCallDate));
            }
        }

        public abstract void Execute();

        protected void Work(Action action)
        {
            while (Run)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("Service {0} started", Model.Name));
                Log.WriteToEventLog("Started service=>" + Model.Name, "INFO");
                var startDate = DateTime.Now;
                try
                {
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("UpdateModel {0} start", Model.Name));
                    UpdateModel();
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("UpdateModel {0} end", Model.Name));
                    if (CheckServiceTime(Model)) // Model.IsActive &&
                    {
                        Model.LastCallKS = startDate;
                        SetLastCallTimes(Model, startDate);
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("action() {0} start", Model.Name));
                        action();
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("action() {0} end", Model.Name));
                        _isSuccess = true;
                    }
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("Service {0} ended", Model.Name));
                }
                catch (Exception ex)
                {
                    _isSuccess = false;

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.GENERAL, string.Format("Service {0} has exception : {1}", Model.Name, ex.Message));
                    LogException(ex, LogId);
                }
                finally
                {
                    if (_isSuccess && _shouldSetLastSuccessCallDate)
                    {
                        Model.LastSuccessCall = startDate;
                    }
                    else
                    {
                        _shouldSetLastSuccessCallDate = true;
                    }
                    Log.WriteToEventLog("End service=>" + Model.Name, "INFO");

                    WaitForNextCall();
                }
            }
        }
    }
}