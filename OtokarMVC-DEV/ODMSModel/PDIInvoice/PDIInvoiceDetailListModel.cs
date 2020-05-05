using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PDIInvoice
{
    public class PDIInvoiceDetailListModel : BaseListWithPagingModel
   {
        public PDIInvoiceDetailListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
        }

        public PDIInvoiceDetailListModel()
        {

        }
        [Display(Name = "PDIInvoiceList_Display_RefNo", ResourceType = typeof(MessageResource))]
        public long RefNo { get; set; }
        [Display(Name = "PDIInvoiceList_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "PDIInvoiceList_Display_PDIRecordNo", ResourceType = typeof(MessageResource))]
        public string PDIRecordNo { get; set; }
        [Display(Name = "PDIInvoiceList_Display_MaintenanceDate", ResourceType = typeof(MessageResource))]
        public DateTime? MaintenanceDate { get; set; }
        [Display(Name = "PDIInvoiceList_Display_OperationDate", ResourceType = typeof(MessageResource))]
        public DateTime? OperationDate { get; set; }
        [Display(Name = "PDIInvoiceList_Display_TotalLabourPrice", ResourceType = typeof(MessageResource))]
        public decimal? TotalLabourPrice { get; set; }
        [Display(Name = "PDIInvoiceList_Display_TotalPartPrice", ResourceType = typeof(MessageResource))]
        public decimal? TotalPartPrice {get;set;}
    }
}
