using ODMSCommon.Security;
using ODMSData;
using ODMSModel.StockBlock;
using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class StockBlockBL : BaseBusiness
    {
        private readonly StockBlockData data = new StockBlockData();

        public ResponseModel<StockBlockListModel> ListStockBlock(UserInfo user,StockBlockListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<StockBlockListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListStockBlock(user,filter, out totalCnt);
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

        public ResponseModel<StockBlockViewModel> GetStockBlock(UserInfo user,StockBlockViewModel filter)
        {
            var response = new ResponseModel<StockBlockViewModel>();
            try
            {
                response.Model = data.GetStockBlock(user,filter);
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

        public ResponseModel<StockBlockViewModel> DMLStockBlock(UserInfo user,StockBlockViewModel model)
        {
            var response = new ResponseModel<StockBlockViewModel>();
            try
            {
                data.DMLStockBlock(user, model);
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
