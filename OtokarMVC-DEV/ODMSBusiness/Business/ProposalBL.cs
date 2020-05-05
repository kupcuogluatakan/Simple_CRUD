using ODMSCommon.Security;
using ODMSData;
using ODMSData.Utility;
using ODMSModel;
using ODMSModel.Proposal;
using ODMSModel.ProposalCard;
using ODMSModel.VehicleBodyWorkProposal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness.Business
{
    public class ProposalBL : BaseBusiness
    {
        private readonly AppointmentTypeData dataAppointmentType = new AppointmentTypeData();
        private readonly DealerData dataDealer = new DealerData();
        private readonly ProposalData data = new ProposalData();
        
        public ResponseModel<SelectListItem> ListAppointmentTypes(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataAppointmentType.ListAppointmentTypeAsSelectListItems(user);
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
        public ResponseModel<SelectListItem> ListAppointmentTypesForProposal(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataAppointmentType.ListAppointmentTypeForProposalAsSelectListItems(user);
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
        public ResponseModel<SelectListItem> GetDealerUsers(int dealerId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataDealer.GetDealerUsersAsSelectListItem(dealerId);
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
        public ResponseModel<ProposalListModel> ListProposal(UserInfo user,ProposalListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ProposalListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListProposal(user,filter, out totalCnt);
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
        public ResponseModel<SelectListItem> ListProposalStats(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListProposalStats(user);
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
        public ResponseModel<ProposalViewModel> DMLProposal(UserInfo user,ProposalViewModel model)
        {
            var response = new ResponseModel<ProposalViewModel>();
            try
            {
                data.DMLProposal(user, model);
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
        public ResponseModel<ProposalCardModel> GetProposalViewModel(UserInfo user,long id,int seq)
        {
            var response = new ResponseModel<ProposalCardModel>();
            try
            {
                response.Model = data.GetProposalCard(user,id, seq);
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
        public ResponseModel<object> GetProposalData(int id, string type)
        {
            var response = new ResponseModel<object>();
            try
            {
                response.Model = data.GetProposalData(id, type);
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
        public ResponseModel<int> CheckFleet(UserInfo user,int customerId, int vehicleId)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.CheckFleet(user, customerId, vehicleId);
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
        public ResponseModel<ModelBase> CancelProposal(UserInfo user,long ProposalCardId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.CancelProposal(user, ProposalCardId);
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
        public ResponseModel<int> GetVehicleCustomerId(int vehicleId)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.GetVehicleCustomerId(vehicleId);
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
        public ResponseModel<CustomerChangeModel> GetCustomerChangeData(int customerId, int vehicleCustomerId)
        {
            var response = new ResponseModel<CustomerChangeModel>();
            try
            {
                response.Model = data.GetCustomerChangeData(customerId, vehicleCustomerId);
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
        public ResponseModel<VehicleBodyworkViewModelProposal> GetBodyworkFromVehicleProposal(VehicleBodyworkViewModelProposal model)
        {
            var response = new ResponseModel<VehicleBodyworkViewModelProposal>();
            try
            {
                data.GetBodyworkFromVehicle(model);
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

        public ResponseModel<ProposalViewModel> ConvertToWorkOrder(UserInfo user,ProposalViewModel filter)
        {
            var response = new ResponseModel<ProposalViewModel>();
            try
            {
                response.Model = data.ConvertToWorkOrder(user,filter);
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
