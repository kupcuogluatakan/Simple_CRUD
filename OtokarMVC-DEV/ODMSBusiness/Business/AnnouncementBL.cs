using System;
using System.Collections.Generic;
using ODMSData;
using ODMSModel.Announcement;
using ODMSModel.ListModel;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class AnnouncementBL : BaseBusiness
    {
        private readonly AnnouncementData data = new AnnouncementData();

        public ResponseModel<AnnouncementListModel> ListAnnouncements(UserInfo user,AnnouncementListModel filter)
        {
            var response = new ResponseModel<AnnouncementListModel>();
            try
            {
                var totalCnt = 0;
                response.Data = data.ListAnnouncements(user,filter, out totalCnt);
                response.Total = totalCnt;
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

        public ResponseModel<AnnouncementViewModel> GetAnnouncement(UserInfo user, AnnouncementViewModel filter)
        {
            var response = new ResponseModel<AnnouncementViewModel>();
            try
            {
                response.Model = data.GetAnnouncement(user, filter);
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

        public ResponseModel<AnnouncementViewModel> DMLAnnouncement(UserInfo user, AnnouncementViewModel model)
        {
            var response = new ResponseModel<AnnouncementViewModel>();
            try
            {
                data.DMLAnnouncement(user, model);
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

        public ResponseModel<UserListModel> ListMailUsers(Int64 annId)
        {
            var response = new ResponseModel<UserListModel>();
            try
            {
                response.Data = data.ListMailUsers(annId);
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
    }
}
