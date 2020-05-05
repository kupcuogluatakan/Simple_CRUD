using System.Collections.Generic;
using ODMSData;
using ODMSModel.ClaimDismantledParts;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class ClaimDismantledPartsBL : BaseBusiness
    {
        private readonly ClaimDismantledPartsData _data = new ClaimDismantledPartsData();

        public ResponseModel<ClaimDismantledPartsListModel> ListClaimDismantledPartss(UserInfo user,ClaimDismantledPartsListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ClaimDismantledPartsListModel>();
            totalCnt = 0;
            try
            {
                response.Data = _data.ListClaimDismantledParts(user,filter, out totalCnt);
                response.Total = totalCnt;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ClaimDismantledPartsViewModel> GetClaimDismantledParts(UserInfo user, ClaimDismantledPartsViewModel filter)
        {
            var response = new ResponseModel<ClaimDismantledPartsViewModel>();
            try
            {
                response.Model = _data.GetClaimDismantledParts(user, filter);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ClaimDismantledPartsViewModel> DMLClaimDismantledParts(UserInfo user, ClaimDismantledPartsViewModel model)
        {
            var response = new ResponseModel<ClaimDismantledPartsViewModel>();
            try
            {
                _data.DMLClaimDismantledParts(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ClaimWaybillViewModel> GetClaimWaybill(ClaimWaybillViewModel model)
        {
            var response = new ResponseModel<ClaimWaybillViewModel>();
            try
            {
                response.Model = _data.GetClaimWaybill(model);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ClaimWaybillViewModel> DMLClaimWaybill(UserInfo user, ClaimWaybillViewModel model)
        {
            var response = new ResponseModel<ClaimWaybillViewModel>();
            try
            {
                _data.DMLClaimWaybill(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ClaimDismantledPartsViewModel> UpdateClaimDismantledParts(UserInfo user, ClaimDismantledPartsViewModel model)
        {
            var response = new ResponseModel<ClaimDismantledPartsViewModel>();
            try
            {
                _data.UpdateClaimDismantledParts(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), _data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }
    }
}
