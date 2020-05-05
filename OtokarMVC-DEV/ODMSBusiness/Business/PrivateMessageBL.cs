using System.Collections.Generic;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.AppointmentIndicatorFailureCode;
using ODMSModel.Common;
using ODMSModel.PrivateMessage;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class PrivateMessageBL:BaseBusiness
    {
        private readonly PrivateMessageData data = new PrivateMessageData();
        public ResponseModel<PrivateMessageListModel> ListMessages(UserInfo user,PrivateMessageListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PrivateMessageListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListMessages(user,filter, out totalCnt);
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

        public ResponseModel<PrivateMessageListModel> GetMessageHistory(UserInfo user, int messageId, int currentPage, out int totalCnt)
        {
            var response = new ResponseModel<PrivateMessageListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetMessageHistory(user,messageId, currentPage, out totalCnt);
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

        public ResponseModel<PrivateMessageModel> SendMessage(UserInfo user,PrivateMessageModel model)
        {
            var response = new ResponseModel<PrivateMessageModel>();
            try
            {
                data.SendMessage(user, model);
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

        public ResponseModel<ComboBoxModel> ListRecievers(UserInfo user, string searchText)
        {
            var response = new ResponseModel<ComboBoxModel>();
            try
            {
                response.Data = data.ListRecievers(user, searchText);
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
