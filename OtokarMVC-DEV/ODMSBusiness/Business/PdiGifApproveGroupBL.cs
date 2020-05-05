using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.PdiGifApproveGroup;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class PdiGifApproveGroupBL : BaseBusiness
    {
        private readonly PdiGifApproveGroupData data = new PdiGifApproveGroupData();

        public ResponseModel<PdiGifApproveGroupListModel> ListPdiGifApproveGroups(UserInfo user, PdiGifApproveGroupListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<PdiGifApproveGroupListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListPdiGifApproveGroups(user, filter, out totalCnt);
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

        public ResponseModel<PdiGifApproveGroupViewModel> DMLPdiGifApproveGroup(UserInfo user, PdiGifApproveGroupViewModel model)
        {
            var response = new ResponseModel<PdiGifApproveGroupViewModel>();
            try
            {
                data.DMLPdiGifApproveGroup(user, model);
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

        public ResponseModel<PdiGifApproveGroupViewModel> GetPdiGifApproveGroup(UserInfo user, int id)
        {
            var response = new ResponseModel<PdiGifApproveGroupViewModel>();
            try
            {
                response.Model = data.GetPdiGifApproveGroup(user, id);
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

        public static ResponseModel<SelectListItem> ListPdiGifApproveGroupsAsSelectItem(UserInfo user)
        {
            var data = new PdiGifApproveGroupData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPdiGifApproveGroupsAsSelectItem(user);
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
