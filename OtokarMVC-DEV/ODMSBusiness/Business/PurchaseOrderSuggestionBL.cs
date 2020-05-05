using ODMSCommon.Security;
using ODMSData;
using ODMSModel.PurchaseOrderSuggestion;
using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class PurchaseOrderSuggestionBL : BaseBusiness
    {
        private readonly PurchaseOrderSuggestionData data = new PurchaseOrderSuggestionData();
        public ResponseModel<PurchaseOrderSuggestionListModel> ListPurchaseOrderSuggestion(UserInfo user,PurchaseOrderSuggestionListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PurchaseOrderSuggestionListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPurchaseOrderSuggestion(user,filter, out totalCnt);
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

        public ResponseModel<PurchaseOrderSuggestionViewModel> PurchaseOrderSuggest(UserInfo user,PurchaseOrderSuggestionViewModel model)
        {
            var response = new ResponseModel<PurchaseOrderSuggestionViewModel>();
            try
            {
                data.PurchaseOrderSuggest(user, model);
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
