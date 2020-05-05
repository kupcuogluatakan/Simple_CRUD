using System.Collections.Generic;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrderBatchInvoice
{
    public class WorkOrderBatchInvoiceList : BaseListWithPagingModel
    {
        public WorkOrderBatchInvoiceList()
        {

        }
        public WorkOrderBatchInvoiceList([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"WorkOrderId", "ID_WORK_ORDER"},
                     {"WorkOrderDetailId", "ID_WORK_ORDER_DETAIL"},
                     {"IndicatorName", "INDICATOR_TYPE_NAME"},
                     {"Plate", "PLATE"},
                     {"VinNo", "VIN_NO"},
                     {"CustomerFirstName","CUSTOMER_NAME"},
                     {"CustomerLastName", "CUSTOMER_LAST_NAME"},
                     {"IndicatorType", "INDICATOR_TYPE"},
                     {"IndicatorTypeName", "INDICATOR_TYPE_NAME"},
                     {"ProcessType", "PROCESS_TYPE"},
                     {"ProcessTypeName", "PROCESS_TYPE_NAME"},
                 };
            SetMapper(dMapper);

        }

        public int DealerId { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_WorkOrderId", ResourceType = typeof(MessageResource))]
        public long WorkOrderId { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_WorkOrderDetailId", ResourceType = typeof(MessageResource))]
        public long WorkOrderDetailId { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_IndicatorName", ResourceType = typeof(MessageResource))]
        public string IndicatorName { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_Plate", ResourceType = typeof(MessageResource))]
        public string Plate { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_CustomerFirstName", ResourceType = typeof(MessageResource))]
        public string CustomerFirstName { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_CustomerLastName", ResourceType = typeof(MessageResource))]
        public string CustomerLastName { get; set; }
        public string IndicatorType { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_IndicatorTypeName", ResourceType = typeof(MessageResource))]
        public string IndicatorTypeName{ get; set; }
        public string ProcessType { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_ProcessTypeName", ResourceType = typeof(MessageResource))]
        public string ProcessTypeName { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_Price", ResourceType = typeof(MessageResource))]
        public decimal Price { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_Currency", ResourceType = typeof(MessageResource))]
        public string Currency { get; set; }
    }
}
