using System.Collections.Generic;
using ODMSData;
using ODMSModel.CampaignDismantlePart;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CampaignDismantlePartBL : BaseBusiness
    {
        private readonly CampaignDismantlePartData data = new CampaignDismantlePartData();

        public ResponseModel<CampaignDismantlePartViewModel> DMLCampaignDismantlePart(UserInfo user, CampaignDismantlePartViewModel model)
        {
            var response = new ResponseModel<CampaignDismantlePartViewModel>();
            try
            {
                data.DMLCampaignDismantlePart(user, model);
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

        public ResponseModel<CampaignDismantlePartViewModel> GetCampaignDismantlePart(UserInfo user, CampaignDismantlePartViewModel filter)
        {
            var response = new ResponseModel<CampaignDismantlePartViewModel>();
            try
            {
                response.Model = data.GetCampaignDismantlePart(user, filter);
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

        public ResponseModel<CampaignDismantlePartListModel> ListCampaignDismantlePart(UserInfo user,CampaignDismantlePartListModel filter, out int totalCount)
        {
            var response = new ResponseModel<CampaignDismantlePartListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListCampaignDismantlePart(user,filter, out totalCount);
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

    }
}
