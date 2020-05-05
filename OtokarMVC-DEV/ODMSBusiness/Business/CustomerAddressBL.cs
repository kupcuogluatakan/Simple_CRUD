using System.Collections.Generic;
using ODMSData;
using ODMSModel.CustomerAddress;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CustomerAddressBL : BaseBusiness
    {
        private readonly CustomerAddressData data = new CustomerAddressData();

        public ResponseModel<CustomerAddressListModel> ListCustomerAddresses(UserInfo user,CustomerAddressListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CustomerAddressListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCustomerAddresses(user,filter, out totalCnt);
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

        public ResponseModel<CustomerAddressIndexViewModel> GetCustomerAddress(UserInfo user, CustomerAddressIndexViewModel filter)
        {
            var response = new ResponseModel<CustomerAddressIndexViewModel>();
            try
            {
                response.Model = data.GetCustomerAddress(user, filter);
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

        public ResponseModel<CustomerAddressIndexViewModel> DMLCustomerAddress(UserInfo user, CustomerAddressIndexViewModel model)
        {
            var response = new ResponseModel<CustomerAddressIndexViewModel>();
            try
            {
                data.DMLCustomerAddress(user, model);
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
