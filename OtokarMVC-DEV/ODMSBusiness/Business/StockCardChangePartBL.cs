using System.Collections.Generic;
using ODMSData;
using ODMSModel.StockCardChangePart;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class StockCardChangePartBL : BaseBusiness
    {
        private readonly StockCardChangePartData data = new StockCardChangePartData();

        public ResponseModel<StockCardChangePartListModel> ListStockCardChangePart(StockCardChangePartListModel filter, out int totalCnt)
        {

            var response = new ResponseModel<StockCardChangePartListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListStockCardChangePart(filter, out totalCnt);
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
