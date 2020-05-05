using System.Collections.Generic;
using ODMSData;
using ODMSModel.AppointmentDetailsParts;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class AppointmentDetailsPartsBL : BaseBusiness
    {
        private readonly AppointmentDetailsPartsData data = new AppointmentDetailsPartsData();

        public ResponseModel<AppointmentDetailsPartsListModel> GetAppointmentDPList(UserInfo user,AppointmentDetailsPartsListModel filter, out int totalCount)
        {
            var response = new ResponseModel<AppointmentDetailsPartsListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetAppointmentDPList(user,filter, out totalCount);
                response.Total = totalCount;
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

        public ResponseModel<AppointmentDetailsPartsViewModel> DMLAppointmentDetailsParts(UserInfo user, AppointmentDetailsPartsViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsPartsViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppointmentDetailsParts(user, model);
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

        public ResponseModel<AppointmentDetailsPartsViewModel> GetAppointmentDP(UserInfo user, AppointmentDetailsPartsViewModel filter)
        {
            var response = new ResponseModel<AppointmentDetailsPartsViewModel>();
            try
            {
                data.GetAppointment(user, filter);
                response.Model = filter;
                response.Message = MessageResource.Global_Display_Success;
                if (filter.ErrorNo > 0)
                    throw new System.Exception(filter.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<AppointmentDetailsPartsViewModel> GetAppIndicType(UserInfo user, AppointmentDetailsPartsViewModel filter)
        {

            var response = new ResponseModel<AppointmentDetailsPartsViewModel>();
            try
            {
                data.GetAppIndicType(filter);
                response.Model = filter;
                response.Message = MessageResource.Global_Display_Success;
                if (filter.ErrorNo > 0)
                    throw new System.Exception(filter.ErrorMessage);
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
