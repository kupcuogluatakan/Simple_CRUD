using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using ODMSModel.WebServiceLogs;
using ODMSData.Utility;
using ODMSData;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class WebServiceLogsBL : BaseBusiness
    {
        private readonly WebServiceLogData data = new WebServiceLogData();

        public ResponseModel<InvoiceListLogItem> ListInvoiceLogs(InvoiceListLogItem request, InvoiceListFilter filter)
        {
            var response = new ResponseModel<InvoiceListLogItem>();
            try
            {
                response.Data = data.ListInvoiceLogs(request, filter);
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

        public ResponseModel<string> GetInvoiceLogResponse(long id)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetInvoiceLogResponse(id);
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
