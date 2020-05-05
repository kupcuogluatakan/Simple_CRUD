using System.Collections.Generic;
using ODMSData;
using ODMSModel.DealerPurchaseOrderPartConfirm;
using ODMSModel.PurchaseOrder;
using ODMSModel.PurchaseOrderDetail;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class DealerPurchaseOrderPartConfirmBL : BaseBusiness
    {
        private readonly DealerPurchaseOrderPartConfirmData data = new DealerPurchaseOrderPartConfirmData();

        public ResponseModel<DealerPurchaseOrderPartConfirmListModel> ListDealerPurchaseOrderPartConfirms(UserInfo user,DealerPurchaseOrderPartConfirmListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerPurchaseOrderPartConfirmListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerPurchaseOrderPartConfirms(user,filter, out totalCnt);
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

        public ResponseModel<PurchaseOrderViewModel> DMLDealerPurchaseOrderPartConfirm(UserInfo user, PurchaseOrderViewModel model, List<PurchaseOrderDetailViewModel> filter)
        {
            var response = new ResponseModel<PurchaseOrderViewModel>();
            try
            {
                data.DMLDealerPurchaseOrderPartConfirm(user, model, filter);
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
