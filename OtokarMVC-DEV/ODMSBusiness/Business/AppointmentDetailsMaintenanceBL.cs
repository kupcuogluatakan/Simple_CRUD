using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.AppointmentDetailsMaintenance;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class AppointmentDetailsMaintenanceBL : BaseBusiness
    {
        private readonly AppointmentDetailsMaintenanceData data = new AppointmentDetailsMaintenanceData();

        public ResponseModel<AppointmentDetailsMaintenanceListModel> GetAppDetailMaintList(UserInfo user,AppointmentDetailsMaintenanceListModel filter, out int totalCount)
        {
            var response = new ResponseModel<AppointmentDetailsMaintenanceListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.GetAppDetailMaintList(user,filter, out totalCount);
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

        public ResponseModel<AppointmentDetailsMaintenanceViewModel> DMLAppIndMainPartsLabours(UserInfo user, AppointmentDetailsMaintenanceViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsMaintenanceViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppIndMainPartsLabours(user, model);
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

        public ResponseModel<ChangePartViewModel> GetSparePart(UserInfo user, ChangePartViewModel filter)
        {
            var response = new ResponseModel<ChangePartViewModel>();
            try
            {
                data.GetSparePart(user, filter);
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

        public ResponseModel<ChangePartViewModel> ReplaceAppIndicatorPart(UserInfo user, ChangePartViewModel filter)
        {
            var response = new ResponseModel<ChangePartViewModel>();
            try
            {
                data.ReplaceAppIndicatorPart(user, filter);
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

        public static ResponseModel<SelectListItem> ListAppMaintenanceAsSelectItem(UserInfo user, int appIndicId)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new AppointmentDetailsMaintenanceData();
            try
            {
                response.Data = data.ListAppMaintenanceAsSelectItem(user, appIndicId);
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

        public ResponseModel<AppointmentDetailsMaintenanceViewModel> DMLAppoinmentIndicatorMaint(UserInfo user, AppointmentDetailsMaintenanceViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsMaintenanceViewModel>();
            try
            {
                response.Model = model;
                data.DMLAppoinmentIndicatorMaint(user, model);
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

        public ResponseModel<AppointmentDetailsMaintenanceViewModel> GetMaintIdByAppIndicId(UserInfo user, AppointmentDetailsMaintenanceViewModel filter)
        {
            var response = new ResponseModel<AppointmentDetailsMaintenanceViewModel>();
            try
            {
                data.GetMaintIdByAppIndicId(user, filter);
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
