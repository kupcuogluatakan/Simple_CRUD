using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.ServiceCallSchedule;
using ODMSCommon.Security;
using System;

namespace ODMSBusiness
{
    public class ServiceCallScheduleBL : BaseService<ServiceCallScheduleViewModel>
    {
        private readonly ServiceCallScheduleData data = new ServiceCallScheduleData();
        private readonly CommonData dataCommon = new CommonData();

        public ResponseModel<ServiceCallScheduleListModel> List(UserInfo user, ServiceCallScheduleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ServiceCallScheduleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.List(user, filter, out totalCnt);
                response.Total = totalCnt;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public override ResponseModel<ServiceCallScheduleViewModel> Get(UserInfo user, ServiceCallScheduleViewModel model)
        {
            var response = new ResponseModel<ServiceCallScheduleViewModel>();
            try
            {
                data.Get(model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ServiceCallScheduleViewModel> Get(string serviceName)
        {
            var model = new ServiceCallScheduleViewModel();
            model.ServiceDescription = serviceName;

            var response = new ResponseModel<ServiceCallScheduleViewModel>();
            try
            {
                data.Get(model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public override ResponseModel<ServiceCallScheduleViewModel> Update(UserInfo user, ServiceCallScheduleViewModel model)
        {
            var response = new ResponseModel<ServiceCallScheduleViewModel>();
            try
            {
                data.Update(model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<string> GetTimeOnly(string p)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetTimeOnly(p);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ServiceCallLogModel> LogRequestService(ServiceCallLogModel model)
        {
            var response = new ResponseModel<ServiceCallLogModel>();
            if (model.ReqResDic != null)
            {
                model.LogXml = new XElement("RequestLog", model.ReqResDic.Select(p => new XElement(p.Key, p.Value)));
            }

            try
            {
                data.LogService(model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ServiceCallLogModel> LogResponseService(ServiceCallLogModel model)
        {
            var response = new ResponseModel<ServiceCallLogModel>();
            try
            {
                data.LogService(model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<bool> RefreshCallSchedule(ServiceScheduleModel model)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.RefreshCallSchedule(model);
                response.Model = true;
                response.Message = MessageResource.Global_Display_Success;

            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<string> SendErrorMail(ServiceCallLogModel logModel)
        {
            var response = new ResponseModel<string>();
            if (!logModel.IsSuccess)
            {
                var dal = new CommonData();
                string body = CommonBL.GetGeneralParameterValue("DMS_ROOT_URL").Model + "WebServiceErrorDisplay/WebServiceErrorDisplayIndex/" + logModel.LogId + " <br/><br/>Alınan Hata : " + logModel.ErrorMessage + "<br/><br/>";
                string subject = string.Format("{0} Entegrasyonunda Hata Oluştu", logModel.ServiceName);

                try
                {
                    response.Model = dataCommon.SendGenericDBMail(string.Empty, subject, body, "SERVICE_FAIL_MAIL_LIST");
                    response.Message = MessageResource.Global_Display_Success;
                }
                catch (System.Exception ex)
                {
                    response.IsSuccess = false;
                    AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                    response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
                }

            }
            return response;
            //logModel.IsSuccess = true;
        }

        public ResponseModel<bool> Start(int serviceId, DateTime startDate)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.Start(serviceId, startDate);
                response.Model = true;
                response.Message = MessageResource.Global_Display_Success;

            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }
    }
}
