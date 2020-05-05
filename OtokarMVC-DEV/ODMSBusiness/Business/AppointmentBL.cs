using System.Collections.Generic;
using ODMSData;
using ODMSModel.Appointment;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using System;

namespace ODMSBusiness
{
    public class AppointmentBL : BaseBusiness
    {
        private readonly AppointmentData data = new AppointmentData();

        public ResponseModel<AppointmentListModel> ListAppointments(UserInfo user, AppointmentListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<AppointmentListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListAppointments(user, filter, out totalCnt);
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

        public ResponseModel<AppointmentViewModel> GetAppointment(UserInfo user, int appointmentId)
        {
            var response = new ResponseModel<AppointmentViewModel>();
            try
            {
                response.Model = data.GetAppointment(user, appointmentId);
                response.Message = MessageResource.Global_Display_Success;
                if (response.Model.ErrorNo > 0)
                    throw new System.Exception(response.Model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<AppointmentViewModel> DMLAppointment(UserInfo user, AppointmentViewModel model)
        {
            var response = new ResponseModel<AppointmentViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppointment(user, model);
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

        public ResponseModel<AppointmentCustomerViewModel> GetAppointmentCustomer(UserInfo user, AppointmentCustomerViewModel filter)
        {
            var response = new ResponseModel<AppointmentCustomerViewModel>();
            try
            {
                data.GetAppointmentCustomer(user, filter);
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

        public ResponseModel<AppointmentVehicleInfo> GetAppointmentVehicleInfo(int vehicleId, int appointmentId)
        {
            var response = new ResponseModel<AppointmentVehicleInfo>();
            try
            {
                response.Model = data.GetAppointmentVehicleInfo(vehicleId, appointmentId);
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

        public object GetAppointmentData(int id, string type, int? appointmentId)
        {
            return data.GetAppointmentData(id, type, appointmentId);
        }

        public object GetAppointmentPeriod(int dealerId, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            return data.GetAppointmentPeriod(dealerId, appointmentDate, appointmentTime);
        }

    }
}
