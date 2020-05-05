using System.Collections.Generic;
using ODMSData;
using ODMSModel.GuaranteeAuthorityGroup;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class GuaranteeAuthorityGroupBL : BaseBusiness
    {
        private readonly GuaranteeAuthorityGroupData data = new GuaranteeAuthorityGroupData();

        public ResponseModel<GuaranteeAuthorityGroupListModel> ListGuaranteeAuthorityGroups(UserInfo user, GuaranteeAuthorityGroupListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGuaranteeAuthorityGroups(user, filter, out totalCnt);
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

        public ResponseModel<GuaranteeAuthorityGroupViewModel> DMLGuaranteeAuthorityGroup(UserInfo user, GuaranteeAuthorityGroupViewModel model)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupViewModel>();
            try
            {
                data.DMLGuaranteeAuthorityGroup(user, model);
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


        public ResponseModel<GuaranteeAuthorityGroupViewModel> GetGuaranteeAuthorityGroup(UserInfo user, int id)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupViewModel>();
            try
            {
                response.Model = data.GetGuaranteeAuthorityGroup(user, id);
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
