using ODMSData;
using ODMSModel.StockRackDetail;
using System.Collections.Generic;
using ODMSData.Utility;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;
using System;

namespace ODMSBusiness
{
    public class StockRackDetailBL : BaseBusiness
    {

        private readonly StockRackDetailData data = new StockRackDetailData();

        public ResponseModel<StockRackDetailListModel> ListEmptyStockRackDetail(UserInfo user, StockRackDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<StockRackDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListEmptyStockRackDetail(user, filter, out totalCnt);
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

        public ResponseModel<StockRackDetailListModel> ListStockRackDetail(UserInfo user, StockRackDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<StockRackDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListStockRackDetail(user, filter, out totalCnt);
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

        public ResponseModel<StockExchangeViewModel> DMLStockExchange(UserInfo user, StockExchangeViewModel model)
        {
            var response = new ResponseModel<StockExchangeViewModel>();
            try
            {
                data.DMLStockExchange(user, model);
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

        public ResponseModel<decimal> GetQuantity(long rackId, long partId)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetQuantity(rackId, partId);
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

        public ResponseModel<decimal> GetMovableQuantity(int dealerId, long partId, int stockTypeId, int fromRackId)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetMovableQuantity(dealerId, partId, stockTypeId, fromRackId);
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

        public ResponseModel<StockRackTypeDetailListModel> ListStockRackTypeDetail(UserInfo user, StockRackTypeDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<StockRackTypeDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListStockRackTypeDetail(user, filter, out totalCnt);
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
