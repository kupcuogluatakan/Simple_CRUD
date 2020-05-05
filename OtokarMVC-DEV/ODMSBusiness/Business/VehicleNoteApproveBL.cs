using System.Collections.Generic;
using ODMSModel.VehicleNoteApprove;
using ODMSData;
using System.Web.Mvc;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class VehicleNoteApproveBL : BaseBusiness
    {
        private readonly VehicleNoteApproveData data = new VehicleNoteApproveData();

        public ResponseModel<VehicleNoteApproveListModel> ListVehicleNoteApprove(UserInfo user, VehicleNoteApproveListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleNoteApproveListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleNoteApprove(user, filter, out totalCnt);
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

        public ResponseModel<VehicleNoteApproveModel> GetVehicleNoteApprove(UserInfo user, VehicleNoteApproveModel filter)
        {
            var response = new ResponseModel<VehicleNoteApproveModel>();
            try
            {
                data.GetVehicleNoteApprove(user, filter);
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

        public ResponseModel<VehicleNoteApproveModel> DeleteVehicleNoteApprove(VehicleNoteApproveModel model)
        {
            var response = new ResponseModel<VehicleNoteApproveModel>();
            try
            {
                data.DeleteVehicleNoteApprove(model);
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

        public ResponseModel<VehicleNoteApproveModel> ApproveVehicleNote(UserInfo user, VehicleNoteApproveModel model)
        {
            var response = new ResponseModel<VehicleNoteApproveModel>();
            try
            {
                data.ApproveVehicleNote(user, model);
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

        public static ResponseModel<SelectListItem> ListVehicleNoteApproveAsSelected(UserInfo user)
        {
            var data = new VehicleNoteApproveData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListVehicleNoteApproveAsSelectListItem(user);
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
