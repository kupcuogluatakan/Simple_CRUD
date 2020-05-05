using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.RolePermission;
using ODMSModel.Shared;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class RolePermissionBL : BaseBusiness
    {
        private readonly RolePermissionData data = new RolePermissionData();

        public ResponseModel<RoleListModel> ListRoles(UserInfo user)
        {
            var response = new ResponseModel<RoleListModel>();
            try
            {
                response.Data = data.ListRoles(user);
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

        public ResponseModel<PermissionListModel> ListPermissionsIncludedInRole(UserInfo user,int roleId)
        {
            var response = new ResponseModel<PermissionListModel>();
            try
            {
                response.Data = data.ListPermissionsIncludedInRole(user, roleId);
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

        public ResponseModel<PermissionListModel> ListPermissionsNotIncludedInRole(UserInfo user,int roleId)
        {
            var response = new ResponseModel<PermissionListModel>();
            try
            {
                response.Data = data.ListPermissionsNotIncludedInRole(user, roleId);
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

        public ResponseModel<SaveModel> Save(UserInfo user,SaveModel model)
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
