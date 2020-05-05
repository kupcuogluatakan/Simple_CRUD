using System.Collections.Generic;
using ODMSBusiness.Reports;
using ODMSData;
using ODMSModel.ClaimDismantledPartDelivery;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class ClaimDismantledPartDeliveryBL : BaseService<ClaimDismantledPartDeliveryViewModel>
    {
        private readonly ClaimDismantledPartDeliveryData _data = new ClaimDismantledPartDeliveryData();

        public override ResponseModel<ClaimDismantledPartDeliveryViewModel> Get(UserInfo user, ClaimDismantledPartDeliveryViewModel model)
        {
            var response = new ResponseModel<ClaimDismantledPartDeliveryViewModel>();
            try
            {
                response.Model = _data.Get(user, model);
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

        public ResponseModel<ClaimDismantledPartDeliveryListModel> List(UserInfo user, ClaimDismantledPartDeliveryListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<ClaimDismantledPartDeliveryListModel>();
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

        public new ResponseModel<ClaimDismantledPartDeliveryViewModel> Insert(UserInfo user, ClaimDismantledPartDeliveryViewModel model)
        {
            var response = new ResponseModel<ClaimDismantledPartDeliveryViewModel>();
            try
            {
                _data.Insert(user, model);
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

        public new ResponseModel<ClaimDismantledPartDeliveryViewModel> Update(UserInfo user, ClaimDismantledPartDeliveryViewModel model)
        {
            var response = new ResponseModel<ClaimDismantledPartDeliveryViewModel>();
            try
            {
                _data.Update(user, model);
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

        public new ResponseModel<ClaimDismantledPartDeliveryViewModel> Delete(UserInfo user, ClaimDismantledPartDeliveryViewModel model)
        {
            var response = new ResponseModel<ClaimDismantledPartDeliveryViewModel>();
            try
            {
                _data.Delete(user, model);
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

        public byte[] PrintWayBill(int claimWayBillId)
        {
            return ReportManager.GetReport(ReportType.ClaimDismantledPartRealReport, claimWayBillId);
        }

        public byte[] PrintWayBillCopy(int claimWayBillId)
        {
            return ReportManager.GetReport(ReportType.ClaimDismantledPartCopyReport, claimWayBillId);
        }

        public byte[] PrintWayBillProforma(int claimWayBillId)
        {
            return ReportManager.GetReport(ReportType.ClaimDismantledPartProformaReport, claimWayBillId);
        }
    }
}
