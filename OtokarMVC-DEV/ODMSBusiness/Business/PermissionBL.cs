using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.AppointmentIndicatorFailureCode;
using ODMSModel.Permission;
using ODMSModel.Shared;
using ODMSModel.Shared;


namespace ODMSBusiness
{
    public class PermissionBL : BaseBusiness
    {
        private readonly PermissionData data = new PermissionData();
        public ResponseModel<PermissionListModel> ListPermissions(UserInfo user, PermissionListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PermissionListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPermissions(user, filter, out totalCnt);
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

        public ResponseModel<PermissionIndexViewModel> GetPermission(UserInfo user, PermissionIndexViewModel filter)
        {
            var response = new ResponseModel<PermissionIndexViewModel>();
            try
            {
                response.Model = data.GetPermission(user, filter);
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

        public ResponseModel<PermissionInfo> GetUserPermissions(UserInfo user, int userId)
        {
            var response = new ResponseModel<PermissionInfo>();
            try
            {
                response.Data = data.GetUserPermissions(user, userId);
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
