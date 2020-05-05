using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.WorkOrderInvoice
{
    public class WorkOrderInvoicesListModel:BaseListWithPagingModel
    {
        public WorkOrderInvoicesListModel(){}
        public WorkOrderInvoicesListModel(Kendo.Mvc.UI.DataSourceRequest request)
            :base(request)
        {
           var dMapper = new Dictionary<string, string>
                 {
                     {"WorkOrderId","ID_WORK_ORDER"},
                     {"WorkOrderInvoiceId","ID_WORK_ORDER_INV"},
                     {"CustomerName","CUSTOMER_ID"},
                     {"InvoiceNo","INVOICE_NO"},
                     {"InvoiceAmout","INVOICE_AMOUT"},
                     {"InvoiceDate","INVOICE_DATE"},
                     {"InvoiceRatio","INVOICE_RATIO"}
                 };
            SetMapper(dMapper);
        }
        public long WorkOrderId { get; set; }

        public long WorkOrderInvoiceId { get; set; }

        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvNo", ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvAmount", ResourceType = typeof(MessageResource))]
        public decimal InvoiceAmount { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvDate", ResourceType = typeof(MessageResource))]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "WorkOrderInvoice_Display_InvoiceRatio", ResourceType = typeof(MessageResource))]
        public decimal InvoiceRatio { get; set; }
        
        [Display(Name = "WorkorderListInvoices_Display_InvSerialNo", ResourceType = typeof(MessageResource))]
        public string InvoiceSerialNo { get; set; }


        public string PriceString { get; set; }
    }
}
