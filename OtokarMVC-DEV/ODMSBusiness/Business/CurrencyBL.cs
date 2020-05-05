using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.Currency;
using ODMSModel.ListModel;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CurrencyBL : BaseBusiness
    {
        private readonly CurrencyData data = new CurrencyData();
        public static ResponseModel<SelectListItem> ListCurrencyAsSelectList(UserInfo user)
        {
            var data = new CurrencyData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListCurrencyAsSelectList(user);
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

        public ResponseModel<CurrencyListModel> ListCurrencys(UserInfo user,CurrencyListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CurrencyListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCurrency(user,filter, out totalCnt);
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

        public ResponseModel<CurrencyIndexViewModel> GetCurrency(UserInfo user, CurrencyIndexViewModel currencyModel)
        {
            var response = new ResponseModel<CurrencyIndexViewModel>();
            try
            {
                response.Model = data.GetCurrency(user, currencyModel);
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

        public ResponseModel<CurrencyIndexViewModel> DMLCurrency(UserInfo user, CurrencyIndexViewModel model)
        {
            var response = new ResponseModel<CurrencyIndexViewModel>();
            try
            {
                data.DMLCurrency(user, model);
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
