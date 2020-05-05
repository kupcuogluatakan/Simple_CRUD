using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.Common;
using ODMSModel.UserRole;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;
using System;
using ODMSModel.RolePermission;

namespace ODMSBusiness
{
    public class UserRoleBL : BaseBusiness
    {
        private UserRoleData data = new UserRoleData();
        private CommonData dataCommon = new CommonData();

        public ResponseModel<SelectListItem> GetUsersList(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataCommon.GetUserListForComboBox(user);
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

        public ResponseModel<ComboBoxModel> GetUserRolesIncluded(UserInfo user, string userId)
        {
            var response = new ResponseModel<ComboBoxModel>();
            try
            {
                if (userId == "0")
                    throw new Exception("Kullanıcı bulanamadı!");

                response.Data = data.GetUserRolesIncluded(user, userId);
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

        public ResponseModel<ComboBoxModel> GetUserRolesExcluded(UserInfo user, string userId)
        {
            var response = new ResponseModel<ComboBoxModel>();
            try
            {
                if (userId == "0")
                    throw new Exception("Kullanıcı bulanamadı!");

                response.Data = data.GetUserRolesExcluded(user, userId);
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

        public ResponseModel<UserRoleViewModel> DMLUserRole(UserInfo user, UserRoleViewModel model)
        {
            var response = new ResponseModel<UserRoleViewModel>();
            try
            {
                data.DLMUserRole(user, model);
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

        public ResponseModel<SaveModel> Save(UserInfo user, SaveModel model)
        {
            var response = new ResponseModel<SaveModel>();
            try
            {
                data.Save(user, model);
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
