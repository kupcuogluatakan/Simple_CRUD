using System.Collections.Generic;
using ODMSData;
using ODMSModel.CampaignDocument;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CampaignDocumentBL : BaseBusiness
    {
        private readonly CampaignDocumentData data = new CampaignDocumentData();

        public ResponseModel<CampaignDocumentListModel> ListCampaignDocuments(UserInfo user,CampaignDocumentListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CampaignDocumentListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCampaignDocuments(user,filter, out totalCnt);
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

        public ResponseModel<CampaignDocumentViewModel> GetCampaignDocument(UserInfo user, CampaignDocumentViewModel filter)
        {
            var response = new ResponseModel<CampaignDocumentViewModel>();
            try
            {
                response.Model = data.GetCampaignDocument(user, filter);
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

        public ResponseModel<CampaignDocumentViewModel> DMLCampaignDocument(UserInfo user, CampaignDocumentViewModel model)
        {
            var response = new ResponseModel<CampaignDocumentViewModel>();
            try
            {
                data.DMLCampaignDocument(user, model);
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
