using System.Collections.Generic;
using ODMSData;
using ODMSModel.GuaranteeCompPartMargin;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class GuaranteeCompPartMarginBL : BaseBusiness
    {
        private readonly GuaranteeCompPartMarginData data = new GuaranteeCompPartMarginData();

        public ResponseModel<GuaranteeCompPartMarginListModel> ListGuaranteeCompPartMargin(UserInfo user, GuaranteeCompPartMarginListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GuaranteeCompPartMarginListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGuaranteeCompPartMargin(user, filter, out totalCnt);
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

        public ResponseModel<GuaranteeCompPartMarginViewModel> DMLGuaranteeCompPartMargin(UserInfo user, GuaranteeCompPartMarginViewModel model)
        {
            var response = new ResponseModel<GuaranteeCompPartMarginViewModel>();
            try
            {
                data.DMLGuaranteeCompPartMargin(user, model);
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

        public ResponseModel<GuaranteeCompPartMarginViewModel> GetGuaranteeCompPartMargin(UserInfo user, int guaranteeCompPartMarginId)
        {
            var response = new ResponseModel<GuaranteeCompPartMarginViewModel>();
            try
            {
                response.Model = data.GetGuaranteeCompPartMarginById(user, guaranteeCompPartMarginId);
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
