using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSModel.WorkOrderInvoiceList;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public class WorkOrderInvoiceListBL : BaseBusiness
    {
        private readonly WorkOrderInvoiceListData data = new WorkOrderInvoiceListData();

        public ResponseModel<WorkOrderInvoiceListListModel> ListWorkOrderInvoiceList(UserInfo user, WorkOrderInvoiceListListModel workOrderInvoiceListListModel, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderInvoiceListListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderInvoiceList(user, workOrderInvoiceListListModel, out totalCnt);
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

        public ResponseModel<WorkOrderInvoiceListListModel> ListWorkOrderInvoiceListCancelled(UserInfo user, WorkOrderInvoiceListListModel workOrderInvoiceListListModel, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderInvoiceListListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderInvoiceListCancelled(user, workOrderInvoiceListListModel, out totalCnt);
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

        public ResponseModel<WorkOrderInvoiceListViewModel> DMLWorkOrderInvoiceList(UserInfo user, WorkOrderInvoiceListViewModel model)
        {
            var response = new ResponseModel<WorkOrderInvoiceListViewModel>();
            try
            {
                data.DMLWorkOrderInvoiceList(user, model);
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
