using System.Collections.Generic;
using ODMSData;
using ODMSModel.StockCardPurchaseOrder;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class StockCardPurchaseOrderBL : BaseBusiness
    {
        private readonly StockCardPurchaseOrderData data = new StockCardPurchaseOrderData();

        public ResponseModel<StockCardPurchaseOrderListModel> ListStockCardPurchaseOrder(UserInfo user, StockCardPurchaseOrderListModel filter, out int totalCnt, out int errorCode, out string errorDesc)
        {
            var response = new ResponseModel<StockCardPurchaseOrderListModel>();
            totalCnt = 0;
            errorCode = 0;
            errorDesc = string.Empty;
            try
            {
                response.Data = data.ListStockCardPurchaseOrder(user, filter, out totalCnt, out errorCode, out errorDesc);
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


    }
}
