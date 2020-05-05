using System.Collections.Generic;
using ODMSData;
using ODMSModel.GuaranteeAuthorityLimitations;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class GuaranteeAuthorityLimitationsBL : BaseBusiness
    {
        private readonly GuaranteeAuthorityLimitationsData data = new GuaranteeAuthorityLimitationsData();

        public ResponseModel<GuaranteeAuthorityLimitationsViewModel> DMLGuaranteeAuthorityLimitations(UserInfo user, GuaranteeAuthorityLimitationsViewModel model)
        {
            var response = new ResponseModel<GuaranteeAuthorityLimitationsViewModel>();
            try
            {
                data.DMLGuaranteeAuthorityLimitations(user, model);
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

        public ResponseModel<GuaranteeAuthorityLimitationsViewModel> GetGuaranteeAuthorityLimitations(UserInfo user, string currencyCode, string modelKod)
        {
            var response = new ResponseModel<GuaranteeAuthorityLimitationsViewModel>();
            try
            {
                response.Model = data.GetGuaranteeAuthorityLimitations(user, currencyCode, modelKod);
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

        public ResponseModel<GuaranteeAuthorityLimitationsListModel> ListGuaranteeAuthorityLimitations(UserInfo user, GuaranteeAuthorityLimitationsListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GuaranteeAuthorityLimitationsListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGuaranteeAuthorityLimitations(user, filter, out totalCnt);
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
