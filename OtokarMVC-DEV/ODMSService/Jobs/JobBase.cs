using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.ServiceCallSchedule;
using Quartz;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ODMSService.Jobs
{
    internal abstract class JobBase 
    {
        protected readonly string UserName;
        protected readonly string Password;
        private readonly string _serviceType;
        public ServiceCallScheduleBL serviceCallScheduleBL { get; set; }
        public bool IsStarted { get; set; }

        protected JobBase()
        {
            UserName = ConfigurationManager.AppSettings.Get("PSSCUser");
            Password = ConfigurationManager.AppSettings.Get("PSSCPass");
            _serviceType = ConfigurationManager.AppSettings.Get("ServiceReferance");
            serviceCallScheduleBL = new ServiceCallScheduleBL();
        }
        //public abstract void Execute();

        protected dynamic GetClient()
        {
            try
            {
                if (_serviceType == "Test")
                    return new OtokarService.ProjectServiceSoapClient();
                return new tr.com.otokar.service.ProjectServiceSoapClient();
            }
            catch (Exception ex)
            {
                if (_serviceType == "Test")
                    return new OtokarService.ProjectServiceSoapClient("ProjectServiceSoap121");
                return new tr.com.otokar.service.ProjectServiceSoapClient("ProjectServiceSoap121");
            }
        }
        public ServiceCallScheduleViewModel StartService(int serviceId)
        {
            var serviceCall = new ServiceCallScheduleBL();
            var model = serviceCall.Get(UserManager.UserInfo, new ServiceCallScheduleViewModel() { ServiceId = serviceId }).Model;
            if (model != null)
                serviceCall.Start(serviceId, DateTime.Now);
            return model;
        }
        public void StartService(ServiceCallScheduleViewModel service)
        {
            serviceCallScheduleBL.Start(service.ServiceId, DateTime.Now);
        }
      

        public void EndService(ServiceCallScheduleViewModel serviceModel)
        {
            serviceModel.CommandType = "U";
            serviceModel.IsTriggerService = false;
            serviceModel.LastSuccessCallDate = DateTime.Now;
            serviceModel.NextCallDate = DateTime.Now.AddMinutes((double)serviceModel.CallIntervalMinute);
            serviceCallScheduleBL.Update(UserManager.UserInfo, serviceModel);
        }
        public void LogResponseService(ServiceCallLogModel callLogModel)
        {
            serviceCallScheduleBL.LogResponseService(callLogModel);
        }
        public void LogRequestService(ServiceCallLogModel callLogModel)
        {
            serviceCallScheduleBL.LogRequestService(callLogModel);
        }
        public void SendErrorMail(ServiceCallLogModel callLogModel)
        {
            serviceCallScheduleBL.SendErrorMail(callLogModel);
        }

        
    }
}
