using System.Collections.Generic;
using ODMSData;
using ODMSModel.CampaignPart;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CampaignPartBL : BaseBusiness
    {
        private readonly CampaignPartData data = new CampaignPartData();
        public ResponseModel<CampaignPartListModel> ListCampaignParts(UserInfo user,CampaignPartListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignPartListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignParts(user,filter, out totalCnt);
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

        public ResponseModel<CampaignPartViewModel> GetCampaignPart(UserInfo user, CampaignPartViewModel filter)
        {
            var response = new ResponseModel<CampaignPartViewModel>();
            try
            {
                response.Model = data.GetCampaignPart(user, filter);
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

        public ResponseModel<CampaignPartViewModel> DMLCampaignPart(UserInfo user, CampaignPartViewModel model)
        {
            var response = new ResponseModel<CampaignPartViewModel>();
            try
            {
                data.DMLCampaignPart(user, model);
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
