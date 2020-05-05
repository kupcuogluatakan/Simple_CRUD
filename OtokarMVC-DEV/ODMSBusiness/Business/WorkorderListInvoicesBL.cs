using System.Collections.Generic;
using ODMSData;
using ODMSModel.WorkorderListInvoices;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class WorkorderListInvoicesBL : BaseBusiness
    {
        private readonly WorkorderListInvoicesData data = new WorkorderListInvoicesData();

        public ResponseModel<WorkorderListInvoicesListModel> GetWorkorderInvoicesList(UserInfo user, WorkorderListInvoicesListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkorderListInvoicesListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetWorkorderInvoicesList(user, filter, out totalCnt);
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


    }
}
