using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.Role;
using ODMSModel.Shared;
using ODMSData.Utility;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class RoleBL : BaseBusiness
    {
        private readonly RoleData data = new RoleData();
        public ResponseModel<RoleListModel> ListRoles(UserInfo user,RoleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<RoleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListRoles(user,filter, out totalCnt);
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
        public ResponseModel<RoleIndexViewModel> GetRole(UserInfo user,RoleIndexViewModel filter)
        {
            var response = new ResponseModel<RoleIndexViewModel>();
            try
            {
                response.Model = data.GetRole(user, filter);
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
        public ResponseModel<RoleIndexViewModel> DMLRole(UserInfo user,RoleIndexViewModel model)
        {
            var response = new ResponseModel<RoleIndexViewModel>();
            try
            {
                data.DMLRole(user, model);
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
        public static ResponseModel<SelectListItem> ListRoleTypeAsSelectListItem(UserInfo user,bool? isSystemRole)
        {
            var data = new RoleData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListRoleTypeAsSelectListItem(user,isSystemRole);
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

        public static ResponseModel<SelectListItem> ListRoleTypeCombo(UserInfo user,bool? isSystemRole)
        {
            var data = new RoleData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListRoleTypeCombo(user, isSystemRole);
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
        public static ResponseModel<SelectListItem> ListRoleTypeComboByUserType(UserInfo user,bool? isSystemRole,bool isTech)
        {
            var data = new RoleData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListRoleTypeComboByUserType(user, isSystemRole,isTech);
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
