using System.Collections.Generic;
using ODMSData;
using ODMSModel.WorkOrderBatchInvoice;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class WorkOrderBatchIvoiceBL:BaseBusiness
    {
        private readonly WorkOrderBatchInvoiceData data = new WorkOrderBatchInvoiceData();

        public ResponseModel<WorkOrderBatchInvoiceList> List(UserInfo user,WorkOrderBatchInvoiceList filter)
        {
            var response = new ResponseModel<WorkOrderBatchInvoiceList>();
            try
            {
                response.Data = data.List(user,filter);
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
