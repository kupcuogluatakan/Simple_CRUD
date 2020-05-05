using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.AppointmentDetails;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class AppointmentDetailsBL : BaseBusiness
    {
        private readonly AppointmentDetailsData data = new AppointmentDetailsData();

        public ResponseModel<AppointmentDetailsViewModel> DMLAppointmentDetails(UserInfo user, AppointmentDetailsViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppointmentDetails(user, model);
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

        public ResponseModel<AppointmentDetailsViewModel> GetAppointmentDetails(UserInfo user, int appointmentIndicatorId)
        {
            var response = new ResponseModel<AppointmentDetailsViewModel>();
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

        public ResponseModel<AppointmentDetailsListModel> ListAppointmentDetails(UserInfo user,AppointmentDetailsListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<AppointmentDetailsListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListAppointmentDetails(user,filter, out totalCnt);
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

        public double GetTotalPriceForAppointment(int appId)
        {
            return data.GetTotalPriceForAppointment(appId);
        }

        public ResponseModel<SelectListItem> GetAppointmentIndicType(UserInfo user, int appointmentId, out int vehicleId)
        {
            var response = new ResponseModel<SelectListItem>();
            vehicleId = 0;
            try
            {
                response.Data = data.GetAppointmentIndicType(user, appointmentId, out vehicleId);
                response.Total = response.Data.Count;
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

        public ResponseModel<SelectListItem> GetCampaignCodeByVehicleId(UserInfo user, int vehicleId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetCampaignCodeByVehicleId(user, vehicleId);
                response.Total = response.Data.Count;
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

        public ResponseModel<SelectListItem> GetMaintCoupon(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetMaintCoupon(user);
                response.Total = response.Data.Count;
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

        public ResponseModel<SelectListItem> GetMaintByVehicle(UserInfo user, int vehicleId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetMainByVehicle(user, vehicleId);
                response.Total = response.Data.Count;
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
