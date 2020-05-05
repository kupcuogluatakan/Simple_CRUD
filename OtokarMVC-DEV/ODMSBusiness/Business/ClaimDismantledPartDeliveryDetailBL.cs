using System.Collections.Generic;
using ODMSData;
using ODMSModel.ClaimDismantledPartDeliveryDetails;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class ClaimDismantledPartDeliveryDetailBL : BaseService<ClaimDismantledPartDeliveryDetailListModel>
    {
        private readonly ClaimDismantledPartDeliveryDetailData _data = new ClaimDismantledPartDeliveryDetailData();

        public ResponseModel<ClaimDismantledPartDeliveryDetailListModel> List(UserInfo user, ClaimDismantledPartDeliveryDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ClaimDismantledPartDeliveryDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = _data.List(user, filter, out totalCnt);
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

        public new ResponseModel<ClaimDismantledPartDeliveryDetailListModel> Exists(UserInfo user, ClaimDismantledPartDeliveryDetailListModel filter)
        {
            var response = new ResponseModel<ClaimDismantledPartDeliveryDetailListModel>();
            try
            {
                response.Model = _data.Exists(user, filter);
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

        public ResponseModel<bool> SetClaimWayBill(UserInfo user, int claimDismantledPartId, int claimWayBillId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                _data.SetClaimWayBill(user, claimDismantledPartId, claimWayBillId);
                response.Model = true;
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
    }
}
