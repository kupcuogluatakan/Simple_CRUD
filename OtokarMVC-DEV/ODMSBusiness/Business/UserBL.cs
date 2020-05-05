using System.Collections.Generic;
using ODMSData;
using ODMSModel.ListModel;
using ODMSModel.User;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class UserBL : BaseBusiness
    {
        private UserData data = new UserData();

        public ResponseModel<UserListModel> ListUsers(UserInfo user, UserListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<UserListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListUsers(user, filter, out totalCnt);
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

        public ResponseModel<UserListModel> ListDealerUsers(UserInfo user, UserListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<UserListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListDealerUsers(user, filter, out totalCnt);
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

        public ResponseModel<UserIndexViewModel> GetUser(UserInfo user, UserIndexViewModel filter)
        {
            var response = new ResponseModel<UserIndexViewModel>();
            try
            {
                response.Model = data.GetUser(user, filter);
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

        public ResponseModel<bool> ConvertUser (int id)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.ConvertUser(id);
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
        public ResponseModel<UserIndexViewModel> GetUserView(UserInfo user, UserIndexViewModel filter)
        {
            var response = new ResponseModel<UserIndexViewModel>();
            try
            {
                response.Model = data.GetUserView(user, filter);
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

        public ResponseModel<UserIndexViewModel> GetUserByTCIdentityNo(UserInfo user, string tcIdentityNo)
        {
            var response = new ResponseModel<UserIndexViewModel>();
            try
            {
                response.Model = data.GetUserByTCIdentityNo(user, tcIdentityNo);
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

        public ResponseModel<bool> GetAnyUserByTCIdentityNo(int userId, string tcIdentityNo)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.GetAnyUserByTCIdentityNo(userId, tcIdentityNo);
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

        public ResponseModel<UserIndexViewModel> GetUserByPassportNo(UserInfo user, string passportNo)
        {
            var response = new ResponseModel<UserIndexViewModel>();
            try
            {
                response.Model = data.GetUserByPassportNo(user, passportNo);
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

        public ResponseModel<bool> GetAnyUserByPassportNo(int userId, string passportNo)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.GetAnyUserByPassportNo(userId, passportNo);
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

        public ResponseModel<UserIndexViewModel> DMLUser(UserInfo user, UserIndexViewModel model)
        {
            var response = new ResponseModel<UserIndexViewModel>();
            try
            {
                data.DMLUser(user, model);
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

        public ResponseModel<UserIndexViewModel> DMLUserPassword(UserInfo user, UserIndexViewModel model)
        {
            var response = new ResponseModel<UserIndexViewModel>();
            try
            {
                data.DMLUserPassword(user, model);
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

        public void UpdateUserLanguage(int userId, string languageCode)
        {
            var boUser = new UserData();
            boUser.UpdateUserLanguage(userId, languageCode);
        }
    }
}
