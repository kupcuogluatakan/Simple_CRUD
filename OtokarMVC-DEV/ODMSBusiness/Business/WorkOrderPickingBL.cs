using System.Collections.Generic;
using ODMSData;
using ODMSModel;
using ODMSModel.WorkOrderPicking;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class WorkOrderPickingBL : BaseBusiness
    {

        private readonly WorkOrderPickingData data = new WorkOrderPickingData();

        public ResponseModel<WorkOrderPickingListModel> ListWorkOrderPicking(UserInfo user, WorkOrderPickingListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderPickingListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderPicking(user, filter, out totalCnt);
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

        public ResponseModel<ModelBase> ChangeWorkOrderStat(UserInfo user, long workOrderId, int stat)
        {
            var response = new ResponseModel<ModelBase>();
            int errorNo;
            string errorMessage;
            try
            {
                data.ChangeWorkOrderPickingStatus(user, workOrderId, stat, out errorNo, out errorMessage);

                response.Model = new ModelBase { ErrorNo = errorNo, ErrorMessage = errorMessage };
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

        public ResponseModel<WorkOrderPickingViewModel> DMLWorkOrderPicking(UserInfo user, WorkOrderPickingViewModel model)
        {
            var response = new ResponseModel<WorkOrderPickingViewModel>();
            try
            {
                data.DMLWorkOrderPicking(user, model);
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

        public ResponseModel<WorkOrderPickingViewModel> GetWorkOrderPicking(WorkOrderPickingViewModel filter)
        {
            var response = new ResponseModel<WorkOrderPickingViewModel>();
            try
            {
                response.Model = data.GetWorkOrderPicking(filter);
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
