using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ODMSData.Utility;
using ODMSData;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class PasswordBL : BaseBusiness
    {
        private readonly PasswordData data = new PasswordData();

        public ResponseModel<int> WrongPasswordInputCountForUser(int userId)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.WrongPasswordInputCountForUser(userId);
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

        public ResponseModel<DateTime?> LastLoginFailDateForUser(int userId)
        {
            var response = new ResponseModel<DateTime?>();
            try
            {
                response.Model = data.LastLoginFailDateForUser(userId);
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

        public ResponseModel<string> GetPasswordForUser(int userId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetPasswordForUser(userId);
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

        public ResponseModel<DateTime?> GetLastPasswordChangeDateForUser(int userId)
        {
            var response = new ResponseModel<DateTime?> ();
            try
            {
                response.Model = data.GetLastPasswordChangeDateForUser(userId);
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

        public ResponseModel<bool> PasswordExistsInLastNPasswords(int n, string password, int userId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.PasswordExistsInLastNPasswords(n, password, userId);
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

        public ResponseModel<bool> UnblockUser(int userId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.UnblockUser(userId);
                response.Model = true;
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
