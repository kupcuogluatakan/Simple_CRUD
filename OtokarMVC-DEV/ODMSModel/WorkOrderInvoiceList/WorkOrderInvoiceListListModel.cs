using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.WorkOrderInvoiceList
{
    public class WorkOrderInvoiceListListModel : BaseListWithPagingModel
    {
        public WorkOrderInvoiceListListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"Plate", "PLATE"},
                    {"InvoiceNo","INVOICE_NO"},
                    {"InvoiceDate", "INVOICE_DATE"},
                    {"TCNo", "TC_NO"},
                    {"IdWorkOrder", "ID_WORK_ORDER"},
                    {"TaxNo", "CUSTOMER_TAX_NO"},
                    {"CustomerName", "CUSTOMER_NAME"},
                    {"CustomerLastName", "CUSTOMER_LAST_NAME"},
                    {"TotalAmount", "SUB_TOTAL"},
                    {"VatAmount", "VAT"},
                    {"GeneralAmount", "TOTAL"}

                };
            SetMapper(dMapper);
        }

        public WorkOrderInvoiceListListModel()
        {
        }

        public Int64? IdCustomer { get; set; }

        public Int64 IdWorkOrderInv { get; set; }

        [Display(Name = "User_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdWorkOrder { get; set; }

        public Int64? IdDealer { get; set; }

        [Display(Name = "InvoiceInfoReport_Display_TCIdentityNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TCNo { get; set; }

        [Display(Name = "InvoiceInfoReport_Display_TaxNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TaxNo { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_CustomerLastName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerLastName { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_InvoiceNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceNo { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_InvoiceDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? InvoiceDate { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_Plate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Plate { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_Total", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalAmount { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_Vat_Total", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal VatAmount { get; set; }

        [Display(Name = "WorkOrderInvoiceList_Display_General_Total", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal GeneralAmount { get; set; }
    }
}
