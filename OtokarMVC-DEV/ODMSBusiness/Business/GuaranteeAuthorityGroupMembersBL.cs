using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.GuaranteeAuthorityGroupMembers;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class GuaranteeAuthorityGroupMemberBL : BaseBusiness
    {
        private readonly GuaranteeAuthorityGroupMemberData data = new GuaranteeAuthorityGroupMemberData();

        public ResponseModel<SelectListItem> ListGuaranteeAuthorityGroupMembersIncluded(int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListGuaranteeAuthorityGroupMembersIncluded(groupId);
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

        public ResponseModel<SelectListItem> ListGuaranteeAuthorityGroupMembersExcluded(int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListGuaranteeAuthorityGroupMembersExcluded(groupId);
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

        public ResponseModel<GuaranteeAuthorityGroupMembersModel> Save(UserInfo user, GuaranteeAuthorityGroupMembersModel model)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupMembersModel>();
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
