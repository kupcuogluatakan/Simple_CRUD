using System.Collections.Generic;
using ODMSData;
using ODMSModel.GuaranteeCompLabourMargin;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{

    public class GuaranteeCompLabourMarginBL : BaseBusiness
    {
        private readonly GuaranteeCompLabourMarginData data = new GuaranteeCompLabourMarginData();

        public ResponseModel<GuaranteeCompLabourMarginListModel> ListGuaranteeCompLabourMargin(UserInfo user, GuaranteeCompLabourMarginListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GuaranteeCompLabourMarginListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGuaranteeCompLabourMargin(user, filter, out totalCnt);
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

        public ResponseModel<GuaranteeCompLabourMarginViewModel> DMLGuaranteeCompLabourMargin(UserInfo user, GuaranteeCompLabourMarginViewModel model)
        {
            var response = new ResponseModel<GuaranteeCompLabourMarginViewModel>();
            try
            {
                data.DMLGuaranteeCompLabourMargin(user, model);
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

        public ResponseModel<GuaranteeCompLabourMarginViewModel> GetGuaranteeCompLabourMargin(UserInfo user, int guaranteeCompLabourMarginId)
        {
            var response = new ResponseModel<GuaranteeCompLabourMarginViewModel>();
            try
            {
                response.Model = data.GetGuaranteeCompLabourMarginById(user, guaranteeCompLabourMarginId);
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
