using ODMSData;
using ODMSModel.DealerPurchaseOrderConfirm;
using System.Collections.Generic;
using ODMSModel.SparePartSale;
using ODMSModel.SparePartSaleDetail;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class DealerPurchaseOrderConfirmBL : BaseBusiness
    {
        private readonly DealerPurchaseOrderConfirmData data = new DealerPurchaseOrderConfirmData();

        public ResponseModel<DealerPurchaseOrderConfirmListModel> ListDealerPurchaseOrderConfirm(UserInfo user,DealerPurchaseOrderConfirmListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<DealerPurchaseOrderConfirmListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerPurchaseOrderConfirm(user,filter, out totalCnt);
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

        public ResponseModel<DealerPurchaseOrderConfirmViewModel> DMLDealerPurchaseOrderConfirm(UserInfo user, DealerPurchaseOrderConfirmViewModel model)
        {
            var response = new ResponseModel<DealerPurchaseOrderConfirmViewModel>();
            try
            {
                data.DMLDealerPurchaseOrderConfirm(user, model);
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

        public ResponseModel<int> DMLDealerPurchaseOrderConfirmSave(UserInfo user, SparePartSaleViewModel model, List<SparePartSaleDetailDetailModel> filter)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.DMLDealerPurchaseOrderConfirmSave(user, model, filter);
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
