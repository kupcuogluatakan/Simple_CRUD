using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.CampaignRequest;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ODMSBusiness
{
    public class CampaignRequestBL : BaseBusiness
    {
        private readonly CampaignRequestData data = new CampaignRequestData();

        public ResponseModel<CampaignRequestListModel> ListCampaignRequest(UserInfo user,CampaignRequestListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignRequestListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignRequest(user,filter, out totalCnt);
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

        public ResponseModel<CampaignRequestViewModel> GetCampaignRequest(UserInfo user, CampaignRequestViewModel filter)
        {
            var response = new ResponseModel<CampaignRequestViewModel>();
            try
            {
                response.Model = data.GetCampaignRequest(user, filter);
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

        public ResponseModel<CampaignRequestViewModel> DMLCampaignRequest(UserInfo user, CampaignRequestViewModel model)
        {
            var response = new ResponseModel<CampaignRequestViewModel>();
            try
            {
                data.DMLCampaignRequest(user, model);
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

        public static ResponseModel<SelectListItem> ListRequestStatusAsSelectListItem()
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new CampaignRequestData();
            try
            {
                response.Data = data.ListRequestStatusAsSelectListItem();
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

        public ResponseModel<CampaignRequestDetailListModel> ListCampaignRequestDetails(UserInfo user,CampaignRequestDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignRequestDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignRequestDetails(user,filter, out totalCnt);
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

        public ResponseModel<CampaignRequestDetailListModel> ListCampaignRequestDetailsAndQuantity(CampaignRequestDetailListModel filter)
        {
            var response = new ResponseModel<CampaignRequestDetailListModel>();
            try
            {
                response.Data = data.ListCampaignRequestDetailsAndQuantity(filter);
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
