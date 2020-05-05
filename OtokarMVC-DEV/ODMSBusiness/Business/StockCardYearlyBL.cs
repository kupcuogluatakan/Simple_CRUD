using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.StockCardYearly;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class StockCardYearlyBL : BaseBusiness
    {
        private readonly StockCardYearlyData data = new StockCardYearlyData();

        public ResponseModel<StockCardYearlyListModel> ListStockCardYearly(UserInfo user, StockCardYearlyListModel filter)
        {
            var response = new ResponseModel<StockCardYearlyListModel>();
            try
            {
                response.Data = data.ListStockCardYearly(user, filter);
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

        public ResponseModel<StockCardYearlyListModel> StartUpdateMonthlyStock(UserInfo user, StockCardYearlyListModel model)
        {
            var response = new ResponseModel<StockCardYearlyListModel>();
            try
            {
                data.StartUpdateMonthlyStock(user, model);
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
