using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.GuaranteeAuthorityGroupIndicatorTransTypes;

namespace ODMSBusiness
{
    public class GuaranteeAuthorityGroupIndicatorTransTypeBL : BaseBusiness
    {
        private readonly GuaranteeAuthorityGroupIndicatorTransTypeData data = new GuaranteeAuthorityGroupIndicatorTransTypeData();

        public ResponseModel<SelectListItem> ListGuaranteeAuthorityGroupIndicatorTransTypesIncluded(UserInfo user, int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListGuaranteeAuthorityGroupIndicatorTransTypesIncluded(user, groupId);
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

        public ResponseModel<SelectListItem> ListGuaranteeAuthorityGroupIndicatorTransTypesExcluded(UserInfo user, int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListGuaranteeAuthorityGroupIndicatorTransTypesExcluded(user, groupId);
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

        public ResponseModel<GuaranteeAuthorityGroupIndicatorTransTypesModel> Save(UserInfo user, GuaranteeAuthorityGroupIndicatorTransTypesModel model)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupIndicatorTransTypesModel>();
            try
            {
                data.Save(user, model);
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
