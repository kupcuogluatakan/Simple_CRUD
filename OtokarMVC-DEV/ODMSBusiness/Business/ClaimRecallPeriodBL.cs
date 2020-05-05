using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.ClaimRecallPeriod;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class ClaimRecallPeriodBL : BaseBusiness
    {
        private readonly ClaimRecallPeriodData data = new ClaimRecallPeriodData();

        public ResponseModel<ClaimRecallPeriodListModel> ListClaimRecallPeriods(UserInfo user,ClaimRecallPeriodListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ClaimRecallPeriodListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListClaimRecallPeriod(user,filter, out totalCnt);
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

        public ResponseModel<ClaimRecallPeriodViewModel> GetClaimRecallPeriod(UserInfo user, ClaimRecallPeriodViewModel filter)
        {
            var response = new ResponseModel<ClaimRecallPeriodViewModel>();
            try
            {
                response.Model = data.GetClaimRecallPeriod(user, filter);
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

        public ResponseModel<ClaimRecallPeriodViewModel> DMLClaimRecallPeriod(UserInfo user, ClaimRecallPeriodViewModel model)
        {
            var response = new ResponseModel<ClaimRecallPeriodViewModel>();
            try
            {
                data.DMLClaimRecallPeriod(user, model);
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

        public void ControlClaimPeriodLastDay()
        {
            try
            {
                data.ControlClaimPeriodLastDay();
            }
            catch (Exception ex)
            {
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
            }
        }

        public void ControlClaimPeriodPartListApprove()
        {
            try
            {
                data.ControlClaimPeriodPartListApprove();
            }
            catch (Exception ex)
            {
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
            }
        }
    }
}
