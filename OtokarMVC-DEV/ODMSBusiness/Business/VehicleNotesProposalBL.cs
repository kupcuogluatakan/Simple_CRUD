using System.Collections.Generic;
using ODMSData;
using ODMSModel.VehicleNoteProposal;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class VehicleNotesProposalBL : BaseBusiness
    {
        private readonly VehicleNotesProposalData data = new VehicleNotesProposalData();

        public ResponseModel<VehicleNotesProposalListModel> ListVehicleNotesProposal(UserInfo user, VehicleNotesProposalListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleNotesProposalListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleNotesProposal(user, filter, out totalCnt);
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

        public ResponseModel<VehicleNotesProposalModel> GetVehicleNotesProposal(UserInfo user, VehicleNotesProposalModel filter)
        {
            var response = new ResponseModel<VehicleNotesProposalModel>();
            try
            {
                data.GetVehicleNotesProposal(user, filter);
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

        public ResponseModel<VehicleNotesProposalModel> DMLVehicleNotesProposal(UserInfo user, VehicleNotesProposalModel model)
        {
            var response = new ResponseModel<VehicleNotesProposalModel>();
            try
            {
                data.DMLVehicleNotesProposal(user, model);
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
