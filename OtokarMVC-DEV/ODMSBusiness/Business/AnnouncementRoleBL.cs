using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.AnnouncementRole;
using System;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class AnnouncementRoleBL : BaseBusiness
    {
        private readonly AnnouncementRoleData data = new AnnouncementRoleData();

        public ResponseModel<AnnouncementRoleListModel> ListAnnouncementRole(UserInfo user, AnnouncementRoleListModel filter)
        {
            var response = new ResponseModel<AnnouncementRoleListModel>();
            try
            {
                response.Data = data.ListAnnouncementRole(user, filter);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<AnnouncementRoleListModel> ListRoleTypeWithoutAnnouncement(UserInfo user, AnnouncementRoleListModel filter)
        {
            var response = new ResponseModel<AnnouncementRoleListModel>();
            try
            {
                response.Data = data.ListRoleTypeWithoutAnnouncement(user, filter);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<AnnouncementRoleSaveModel> SaveAnnouncementRole(UserInfo user, AnnouncementRoleSaveModel model)
        {
            var response = new ResponseModel<AnnouncementRoleSaveModel>();
            try
            {
                data.SaveAnnouncementRole(user, model);
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
