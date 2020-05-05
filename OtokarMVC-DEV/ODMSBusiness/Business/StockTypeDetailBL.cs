using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.CycleCountResult;
using ODMSModel.StockTypeDetail;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class StockTypeDetailBL : BaseBusiness
    {
        private readonly StockTypeDetailData data = new StockTypeDetailData();

        public ResponseModel<StockTypeDetailListModel> ListStockTypeDetail(UserInfo user, StockTypeDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<StockTypeDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListStockTypeDetail(user, filter, out totalCnt);
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

        public ResponseModel<CycleCountResultPrototypeViewModel> ListStokTypeAudit(UserInfo user, CycleCountResultAuditViewModel filter)
        {
            var response = new ResponseModel<CycleCountResultPrototypeViewModel>();
            try
            {
                response.Data = data.ListStokTypeAudit(user, filter);
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

        public ResponseModel<decimal> GetStockTypeDetailTotalQuantity(int warehouseId, int partId)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetStockTypeDetailTotalQuantity(warehouseId, partId);
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

