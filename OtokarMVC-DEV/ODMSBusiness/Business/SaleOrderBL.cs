using ODMSCommon.Security;
using ODMSData.Utility;
using ODMSModel.SaleOrder;
using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel;
using ODMSData;

namespace ODMSBusiness.Business
{
    public class SaleOrderBL : BaseBusiness
    {
        private readonly SaleOrderData data = new SaleOrderData();
        
        public ResponseModel<SelectListItem> ListSaleOrderCustomers(UserInfo user,int? dealerId = null)
        {
            if (!dealerId.HasValue)
                dealerId = user.GetUserDealerId();

            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListSaleOrderCustomers(user, dealerId);
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

        public ResponseModel<SelectListItem> ListPartTypes()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = SparePartBL.ListPartTypes();
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

        public ResponseModel<SelectListItem> ListPurchaseOrderTypes(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPurchaseOrderTypes(user);
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

        public ResponseModel<SaleOrderRemainingListItem> ListSaleOrderRemaining(UserInfo user,SaleOrderRemainingFilter filter, out int totalCnt)
        {
            var response = new ResponseModel<SaleOrderRemainingListItem>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSaleOrderRemaining(user,filter, out totalCnt);
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

        public ResponseModel<SaleOrderPartStockInfo> ListSelectedSaleOrderPartsStockQuants(string argument)
        {
            var response = new ResponseModel<SaleOrderPartStockInfo>();
            try
            {
                response.Data = data.ListSelectedSaleOrderPartsStockQuants(argument);
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

        public ResponseModel<ModelBase> CreateSaleOrderDocument(UserInfo user,List<SaleOrderRemainingListItem> list)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.CreateSaleOrderDocument(user,list);
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
