using System.Collections.Generic;
using ODMSData;
using ODMSModel.Announcement;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class ActiveAnnouncementsBL : BaseBusiness
    {
        private readonly ActiveAnnouncementsData data = new ActiveAnnouncementsData();

        public ResponseModel<AnnouncementListModel> ListActiveAnnouncements(UserInfo user, bool IsSlide = false )
        {
            var response = new ResponseModel<AnnouncementListModel>();
            try
            {
                response.Data = data.ListActiveAnnouncements(user,IsSlide);
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

        public ResponseModel<int> GetActiveAnnouncementCount(UserInfo user, out int newMessageCount)
        {
            var response = new ResponseModel<int>();
            newMessageCount = 0;
            try
            {
                response.Model = data.GetActiveAnnouncementCount(user, out newMessageCount);
                response.Total = response.Model;
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
