using System.Collections.Generic;
using ODMSData;
using ODMSModel.MaintenanceAppointment;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class MaintenanceAppointmentBL : BaseBusiness
    {
        private readonly MaintenanceAppointmentData data = new MaintenanceAppointmentData();

        public ResponseModel<MaintenanceAppointmentListModel> GetMaintenanceAppointmentList(UserInfo user, MaintenanceAppointmentListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<MaintenanceAppointmentListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetMaintenanceAppointmentList(user, filter, out totalCnt);
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

        public ResponseModel<MaintenanceAppointmentViewModel> DMLMaintenanceAppointment(UserInfo user, MaintenanceAppointmentViewModel model)
        {
            var response = new ResponseModel<MaintenanceAppointmentViewModel>();
            try
            {
                data.DMLMaintenanceAppointment(user, model);
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


    }
}
