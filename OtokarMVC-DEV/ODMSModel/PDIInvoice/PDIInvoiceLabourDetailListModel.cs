using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PDIInvoice
{
    public class PDIInvoiceLabourDetailListModel : BaseListWithPagingModel
   {
        public PDIInvoiceLabourDetailListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
        }

        public PDIInvoiceLabourDetailListModel()
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
        [Display(Name = "PDIInvoiceList_Display_MaintenanceCode", ResourceType = typeof(MessageResource))]
        public string MaintenanceCode { get; set; }
        [Display(Name = "PDIInvoiceList_Display_Price", ResourceType = typeof(MessageResource))]
        public decimal? Price {get;set;}
    }
}
