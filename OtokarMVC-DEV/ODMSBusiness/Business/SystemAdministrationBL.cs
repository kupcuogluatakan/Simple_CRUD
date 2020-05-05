using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.SystemAdministration;
using System;

namespace ODMSBusiness
{
    public class SystemAdministrationBL : BaseBusiness
    {
        private readonly SystemAdministrationData data = new SystemAdministrationData();

        public ResponseModel<UserInfo> Login(SystemAdministrationLoginModel LoginModel)
        {
            var response = new ResponseModel<UserInfo>();
            try
            {
                response.Model = data.Login(LoginModel);
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

        public ResponseModel<AccountRecoveryViewModel> SetAccountRecovery(AccountRecoveryViewModel model)
        {
            var response = new ResponseModel<AccountRecoveryViewModel>();
            try
            {
                model.Password = CommonUtility.GeneratePassword();

                data.SetAccountRecovery(model);
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

        public ResponseModel<DateTime?> AccountRecoveryTry(string ip)
        {
            var response = new ResponseModel<DateTime?>();
            try
            {
                response.Model = data.AccountRecoveryTry(ip);
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

        public ResponseModel<UserInfo> UpdateSessionData(UserInfo model)
        {
            var response = new ResponseModel<UserInfo>();
            try
            {
                data.UpdateSessionData(model);
                response.Model = model;
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
