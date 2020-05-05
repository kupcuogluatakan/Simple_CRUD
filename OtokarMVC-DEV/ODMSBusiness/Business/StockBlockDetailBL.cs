using ODMSCommon.Security;
using ODMSData;
using ODMSModel.StockBlockDetail;
using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class StockBlockDetailBL : BaseBusiness
    {
        private readonly StockBlockDetailData data = new StockBlockDetailData();

        public ResponseModel<StockBlockDetailListModel> ListStockBlockDetail(UserInfo user,StockBlockDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<StockBlockDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListStockBlockDetail(user,filter, out totalCnt);
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

        public ResponseModel<StockBlockDetailViewModel> GetStockBlockDetail(UserInfo user,StockBlockDetailViewModel filter)
        {
            var response = new ResponseModel<StockBlockDetailViewModel>();
            try
            {
                response.Model = data.GetStockBlockDetail(user, filter);
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

        public ResponseModel<StockBlockDetailViewModel> GetStockBlockDetails(UserInfo user,StockBlockDetailViewModel filter)
        {
            var response = new ResponseModel<StockBlockDetailViewModel>();
            try
            {
                response.Model = data.GetStockBlockDetails(user, filter);
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

        public ResponseModel<StockBlockDetailViewModel> DMLStockBlockDetail(UserInfo user,StockBlockDetailViewModel model)
        {
            var response = new ResponseModel<StockBlockDetailViewModel>();
            try
            {
                data.DMLStockBlockDetail(user, model);
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

        public ResponseModel<StockBlockDetailViewModel> GetStockBlockDetailList(UserInfo user,StockBlockDetailViewModel filter)
        {
            var response = new ResponseModel<StockBlockDetailViewModel>();
            try
            {
                response.Data = data.GetStockBlockDetailList(user, filter);
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

        public ResponseModel<StockBlockDetailViewModel> DMLStockBlockDetailList(UserInfo user,List<StockBlockDetailViewModel> stockBlockListModel, StockBlockDetailViewModel errorModel)
        {
            var response = new ResponseModel<StockBlockDetailViewModel>();
            try
            {
                data.DMLStockBlockDetailList(user,stockBlockListModel, errorModel);
                response.Model = errorModel;
                response.Message = MessageResource.Global_Display_Success;
                if (errorModel.ErrorNo > 0)
                    throw new System.Exception(errorModel.ErrorMessage);
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
