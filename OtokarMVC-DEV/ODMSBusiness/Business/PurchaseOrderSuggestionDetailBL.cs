using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.PurchaseOrderSuggestionDetail;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class PurchaseOrderSuggestionDetailBL : BaseBusiness
    {
        private readonly PurchaseOrderSuggestionDetailData data = new PurchaseOrderSuggestionDetailData();
        public ResponseModel<POSuggestionDetailListModel> ListPOSuggestionDetail(UserInfo user,POSuggestionDetailListModel filter,out int totalCnt)
        {
            var response = new ResponseModel<POSuggestionDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPOSuggestionDetail(user,filter, out totalCnt);
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

        public ResponseModel<POSuggestionDetailViewModel> GetInitialInfoSuggestionDetail(POSuggestionDetailViewModel model)
        {
            var response = new ResponseModel<POSuggestionDetailViewModel>();
            try
            {
                data.GetInitialInfoSuggestionDetail(model);
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

        public void DMLPOSuggestionDetail(UserInfo user,POSuggestionDetailViewModel model)
        {
            
            var response = new ResponseModel<POSuggestionDetailViewModel>();
            try
            {
                data.DMLPurchaseOrder(user, model);
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
            
            if (model.PoNumber>0)
            {
                data.DMLPurchaseOrderDetail(user,model);
            }
            else
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Global_DB_OrderFail;
            }
        }

        public ResponseModel<bool> ControlMrP()
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.ControlMrp();
                response.Model = true;
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

        public ResponseModel<bool> UpdatePurchaseOrderSuggestionDetail(UserInfo user,int mrpId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.UpdatePurchaseOrderSuggestionDetail(user, mrpId);
                response.Model = true;
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
