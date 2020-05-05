﻿using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.PdiGifApproveGroupMembers;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class PdiGifApproveGroupMembersBL : BaseBusiness
    {
        private readonly PdiGifApproveGroupMembersData data = new PdiGifApproveGroupMembersData();

        public ResponseModel<SelectListItem> ListPdiGifApproveGroupMembersIncluded(int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPdiGifApproveGroupMembersIncluded(groupId);
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

        public ResponseModel<SelectListItem> ListPdiGifApproveGroupMembersExcluded(int groupId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListPdiGifApproveGroupMembersExcluded(groupId);
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

        public ResponseModel<PdiGifApproveGroupMembersModel> Save(UserInfo user, PdiGifApproveGroupMembersModel model)
        {
            var response = new ResponseModel<PdiGifApproveGroupMembersModel>();
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
