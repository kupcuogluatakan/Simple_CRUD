using System.Collections.Generic;
using ODMSData;
using ODMSModel.AppointmentDetails;
using ODMSModel.AppointmentDetailsLabours;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class AppointmentDetailsLaboursBL : BaseBusiness
    {
        private readonly AppointmentDetailsLaboursData data = new AppointmentDetailsLaboursData();

        public ResponseModel<AppointmentDetailsViewModel> GetIndicatorData(UserInfo user, int appointmentIndicatorId)
        {
            var response = new ResponseModel<AppointmentDetailsViewModel>();
            var data = new AppointmentDetailsData();
            try
            {
                response.Model = data.GetAppointmentDetails(user, appointmentIndicatorId);
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

        public ResponseModel<AppointmentDetailsLaboursViewModel> DMLAppointmentDetailLabours(UserInfo user, AppointmentDetailsLaboursViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsLaboursViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppointmentDetailLabours(user, model);
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

        public ResponseModel<AppointmentDetailsLaboursListModel> ListAppointmentIndicatorLabours(UserInfo user,AppointmentDetailsLaboursListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<AppointmentDetailsLaboursListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListAppointmentIndicatorLabours(user,filter, out totalCnt);
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

        public ResponseModel<AppointmentDetailsLaboursViewModel> GetAppointmentDetailsLabour(UserInfo user, int id)
        {
            var response = new ResponseModel<AppointmentDetailsLaboursViewModel>();
            try
            {
                response.Model = data.GetAppointmentDetailsLabour(user, id);
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

        public ResponseModel<AppointmentDetailsLaboursViewModel> GetAppIndicType(AppointmentDetailsLaboursViewModel filter)
        {
            var response = new ResponseModel<AppointmentDetailsLaboursViewModel>();
            try
            {
                data.GetAppIndicType(filter);
                response.Model = filter;
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
