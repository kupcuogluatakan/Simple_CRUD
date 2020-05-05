using System.Collections.Generic;
using ODMSData;
using ODMSModel.CustomerDiscount;
using System.Web.Mvc;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class CustomerDiscountBL : BaseBusiness
    {
        private readonly CustomerDiscountData data = new CustomerDiscountData();

        public ResponseModel<CustomerDiscountListModel> ListCustomerDiscount(UserInfo user,CustomerDiscountListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CustomerDiscountListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCustomerDiscount(user,filter, out totalCnt);
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

        public ResponseModel<CustomerDiscountIndexViewModel> GetCustomerDiscount(UserInfo user, CustomerDiscountIndexViewModel filter)
        {
            var response = new ResponseModel<CustomerDiscountIndexViewModel>();
            try
            {
                response.Model = data.GetCustomerDiscount(user, filter);
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

        public ResponseModel<CustomerDiscountIndexViewModel> DMLCustomerDiscount(UserInfo user, CustomerDiscountIndexViewModel model)
        {
            var response = new ResponseModel<CustomerDiscountIndexViewModel>();
            try
            {
                data.DMLCustomerDiscount(user, model);
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

        public ResponseModel<SelectListItem> ListDealers()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDealerAsSelectListItem();
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

    }
}
