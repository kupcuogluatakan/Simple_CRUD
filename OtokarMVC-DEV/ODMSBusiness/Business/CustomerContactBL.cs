using System.Collections.Generic;
using ODMSData;
using ODMSModel.CustomerContact;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class CustomerContactBL : BaseBusiness
    {
        private readonly CustomerContactData data = new CustomerContactData();

        public ResponseModel<CustomerContactListModel> ListCustomerContactes(UserInfo user,CustomerContactListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CustomerContactListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListCustomerContacts(user,filter, out totalCnt);
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

        public ResponseModel<CustomerContactIndexViewModel> GetCustomerContact(UserInfo user, CustomerContactIndexViewModel filter)
        {
            var response = new ResponseModel<CustomerContactIndexViewModel>();
            try
            {
                response.Model = data.GetCustomerContact(user,filter);
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

        public ResponseModel<CustomerContactIndexViewModel> DMLCustomerContact(UserInfo user, CustomerContactIndexViewModel model)
        {
            var response = new ResponseModel<CustomerContactIndexViewModel>();
            try
            {
                data.DMLCustomerContact(user, model);
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
