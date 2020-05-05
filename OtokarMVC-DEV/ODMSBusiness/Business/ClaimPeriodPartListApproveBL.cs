using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.Utility;
using ODMSModel;
using ODMSModel.ClaimPartListApprove;
using ODMSModel.Common;
using ODMSData;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class ClaimPeriodPartListApproveBL : BaseBusiness
    {
        private readonly ClaimPeriodPartListApproveData data = new ClaimPeriodPartListApproveData();

        public ResponseModel<SelectListItem> GetClaimPeriods()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetClaimPeriods();
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

        public ResponseModel<ClaimPartListApproveListModel> ListClaimPeriodParts(UserInfo user, int id)
        {
            var response = new ResponseModel<ClaimPartListApproveListModel>();
            try
            {
                response.Data = data.ListClaimRecallPeriodPart(user, id);
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

        public ResponseModel<ClaimDismantledPartInfoModel> ListDismantledParts(UserInfo user, int claimPeriodId, long partId)
        {
            var response = new ResponseModel<ClaimDismantledPartInfoModel>();
            try
            {
                response.Data = data.ListDismantledParts(user, claimPeriodId, partId);
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

        public ResponseModel<ModelBase> Save(UserInfo user, int claimPeriodId, List<long> selectedParts, List<long> notSelectedParts)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.Save(user, claimPeriodId, selectedParts, notSelectedParts); ;
                response.Message = MessageResource.Global_Display_Success;
                if (response.Model.ErrorNo > 0)
                    throw new System.Exception(response.Model.ErrorMessage);
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
