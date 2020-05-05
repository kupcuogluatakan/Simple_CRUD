using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.CampaignRequestApprove;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CampaignRequestApproveBL : BaseBusiness
    {
        private readonly CampaignRequestApproveData data = new CampaignRequestApproveData();
        public static ResponseModel<SelectListItem> ListSupplierDealerAsSelectListItem(int campaignRequestId, int requiredQuantity)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new CampaignRequestApproveData();
            try
            {
                response.Data = data.ListSupplierDealerAsSelectListItem(campaignRequestId, requiredQuantity);
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

        public ResponseModel<CampaignRequestApproveListModel> ListCampaignRequestApprove(UserInfo user, CampaignRequestApproveListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignRequestApproveListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignRequestApprove(user,filter, out totalCnt);
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

        public ResponseModel<CampaignRequestApproveViewModel> GetCampaignRequestApprove(UserInfo user,CampaignRequestApproveViewModel filter)
        {
            var response = new ResponseModel<CampaignRequestApproveViewModel>();
            try
            {
                response.Model = data.GetCampaignRequestApprove(user,filter);
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

        public ResponseModel<CampaignRequestApproveViewModel> DMLCampaignRequestApprove(UserInfo user,CampaignRequestApproveViewModel model)
        {
            var response = new ResponseModel<CampaignRequestApproveViewModel>();
            try
            {
                data.DMLCampaignRequestApprove(user,model);
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


        public ResponseModel<CampaignRequestApproveJsonModel> ListCampaignRequestVinApprovedCounts(UserInfo user, CampaignRequestApproveJsonModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignRequestApproveJsonModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignRequestVinApprovedCounts(user, filter, out totalCnt);
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


    }
}
