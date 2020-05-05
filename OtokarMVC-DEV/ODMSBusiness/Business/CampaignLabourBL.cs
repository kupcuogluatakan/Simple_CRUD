using System.Collections.Generic;
using ODMSData;
using ODMSModel.CampaignLabour;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CampaignLabourBL : BaseBusiness
    {
        private readonly CampaignLabourData data = new CampaignLabourData();

        public ResponseModel<CampaignLabourListModel> ListCampaignLabours(UserInfo user,CampaignLabourListModel currencyListModel, out int totalCnt)
        {
            var response = new ResponseModel<CampaignLabourListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignLabours(user,currencyListModel, out totalCnt);
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

        public ResponseModel<CampaignLabourViewModel> GetCampaignLabour(UserInfo user, CampaignLabourViewModel filter)
        {
            var response = new ResponseModel<CampaignLabourViewModel>();
            try
            {
                response.Model = data.GetCampaignLabour(user, filter);
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

        public ResponseModel<CampaignLabourViewModel> DMLCampaignLabour(UserInfo user, CampaignLabourViewModel model)
        {
            var response = new ResponseModel<CampaignLabourViewModel>();
            try
            {
                data.DMLCampaignLabour(user, model);
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

        public ResponseModel<SelectListItem> ListLabourTimeAsSelectList(UserInfo user, string campaignCode, long idLabour)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListLabourTimeAsSelectList(user, campaignCode, idLabour);
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
