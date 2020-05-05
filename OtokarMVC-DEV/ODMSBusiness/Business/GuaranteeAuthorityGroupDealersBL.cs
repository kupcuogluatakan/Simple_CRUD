using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.GuaranteeAuthorityGroupDealers;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class GuaranteeAuthorityGroupDealersBL : BaseBusiness
    {
        private readonly GuaranteeAuthorityGroupDealersData data = new GuaranteeAuthorityGroupDealersData();

        public ResponseModel<GuaranteeAuthorityGroupDealersListModel> ListGuaranteeAuthorityGroupDealers(GuaranteeAuthorityGroupDealersListModel filter)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupDealersListModel>();
            try
            {
                response.Data = data.ListGuaranteeAuthorityGroupDealers(filter);
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

        public ResponseModel<GuaranteeAuthorityGroupDealersListModel> ListGuaranteeAuthorityGroupDealersNotInclude(GuaranteeAuthorityGroupDealersListModel filter)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupDealersListModel>();
            try
            {
                response.Data = data.ListGuaranteeAuthorityGroupDealersNotInclude(filter);
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

        public ResponseModel<GuaranteeAuthorityGroupDealersSaveModel> SaveGuaranteeAuthorityGroupDealers(UserInfo user, GuaranteeAuthorityGroupDealersSaveModel model)
        {
            var response = new ResponseModel<GuaranteeAuthorityGroupDealersSaveModel>();
            try
            {
                data.SaveGuaranteeAuthorityGroupDealers(user, model);
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
