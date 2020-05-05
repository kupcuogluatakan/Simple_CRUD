using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.WorkOrderPicking;
using ODMSModel.WorkOrderPickingDetail;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class WorkOrderPickingDetailBL : BaseBusiness
    {
        private readonly WorkOrderPickingDetailData data = new WorkOrderPickingDetailData();

        public ResponseModel<WorkOrderPickingDetailListModel> ListWorkOrderPickingDetail(UserInfo user, WorkOrderPickingDetailListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderPickingDetailListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderPickingDetail(user, filter, out totalCnt);
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

        public ResponseModel<WOPDetSubListModel> ListWorkOrderPickingDetailSub(UserInfo user, WOPDetSubListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WOPDetSubListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderPickingDetailSub(user, filter, out totalCnt);
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

        public ResponseModel<SelectListItem> ListRackWarehouseByDetId(UserInfo user, int id)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListRackWarehouseByDetId(user, id);
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

        public ResponseModel<WOPDetSubViewModel> DMLWOPDetSub(UserInfo user, WOPDetSubViewModel model)
        {
            var response = new ResponseModel<WOPDetSubViewModel>();
            try
            {
                data.DMLWOPDetSub(user, model);
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

        public ResponseModel<WorkOrderPickingViewModel> CompleteWorkOrderPicking(UserInfo user, WorkOrderPickingViewModel model)
        {
            var response = new ResponseModel<WorkOrderPickingViewModel>();
            try
            {
                data.CompleteWorkOrderPicking(user, model);
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

        public ResponseModel<WOPDetSubViewModel> DeleteWOPDetSub(UserInfo user, WOPDetSubViewModel model)
        {
            var response = new ResponseModel<WOPDetSubViewModel>();
            try
            {
                data.DeleteWOPDetSub(user, model);
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

        public ResponseModel<bool> StockCardDefaultRackReturn(UserInfo user, int partId, out int value, out string text)
        {
            var response = new ResponseModel<bool>();
            value = 0;
            text = string.Empty;
            try
            {
                data.StockCardDefaultRackReturn(user, partId, out value, out text);
                response.Model = true;
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

        public ResponseModel<WorkOrderPickingViewModel> WorkOrderPickingDetailRack(UserInfo user, WorkOrderPickingViewModel model)
        {
            var response = new ResponseModel<WorkOrderPickingViewModel>();
            try
            {
                data.WorkOrderPickingDetailRack(user, model);
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

        public ResponseModel<WorkOrderPickingDetailViewModel> DMLWorkOrderPickingDetail(UserInfo user, WorkOrderPickingDetailViewModel model)
        {
            var response = new ResponseModel<WorkOrderPickingDetailViewModel>();
            try
            {
                data.DMLWorkOrderPickingDetail(user, model);
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
