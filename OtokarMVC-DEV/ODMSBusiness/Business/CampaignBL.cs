using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.Campaign;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class CampaignBL : BaseBusiness
    {
        private readonly CampaignData data = new CampaignData();

        public static ResponseModel<SelectListItem> ListCampaignAsSelectListItem(UserInfo user, string modelKod)
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new CampaignData();
            try
            {
                response.Data = data.ListCampaignAsSelectListItem(user, modelKod);
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

        public static ResponseModel<SelectListItem> ListAllCampaignAsSelectListItem()
        {
            var response = new ResponseModel<SelectListItem>();
            var data = new CampaignData();
            try
            {
                response.Data = data.ListAllCampaignAsSelectListItem();
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

        public ResponseModel<CampaignListModel> ListCampaigns(UserInfo user,CampaignListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaign(user,filter, out totalCnt);
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

        public ResponseModel<CampaignViewModel> GetCampaign(UserInfo user, CampaignViewModel filter)
        {
            var response = new ResponseModel<CampaignViewModel>();
            try
            {
                response.Model = data.GetCampaign(user, filter);
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

        public ResponseModel<CampaignViewModel> DMLCampaign(UserInfo user, CampaignViewModel model)
        {
            var response = new ResponseModel<CampaignViewModel>();
            try
            {
                data.DMLCampaign(user, model);
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
