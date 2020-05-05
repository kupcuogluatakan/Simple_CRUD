using System.Collections.Generic;
using ODMSModel.VehicleNoteProposalApprove;
using ODMSData;
using System.Web.Mvc;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class VehicleNoteProposalApproveBL : BaseBusiness
    {
        private readonly VehicleNoteProposalApproveData data = new VehicleNoteProposalApproveData();

        public ResponseModel<VehicleNoteProposalApproveListModel> ListVehicleNoteProposalApprove(UserInfo user, VehicleNoteProposalApproveListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleNoteProposalApproveListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleNoteProposalApprove(user, filter, out totalCnt);
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

        public ResponseModel<VehicleNoteProposalApproveModel> GetVehicleNoteProposalApprove(UserInfo user, VehicleNoteProposalApproveModel filter)
        {
            var response = new ResponseModel<VehicleNoteProposalApproveModel>();
            try
            {
                data.GetVehicleNoteProposalApprove(user, filter);
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

        public ResponseModel<VehicleNoteProposalApproveModel> DeleteVehicleNoteProposalApprove(VehicleNoteProposalApproveModel model)
        {
            var response = new ResponseModel<VehicleNoteProposalApproveModel>();
            try
            {
                data.DeleteVehicleNoteProposalApprove(model);
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

        public ResponseModel<VehicleNoteProposalApproveModel> ApproveVehicleNoteProposal(UserInfo user, VehicleNoteProposalApproveModel model)
        {
            var response = new ResponseModel<VehicleNoteProposalApproveModel>();
            try
            {
                data.ApproveVehicleNoteProposal(user, model);
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

        public static ResponseModel<SelectListItem> ListVehicleNoteProposalApproveAsSelected(UserInfo user)
        {
            var data = new VehicleNoteProposalApproveData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListVehicleNoteProposalApproveAsSelectListItem(user);
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
