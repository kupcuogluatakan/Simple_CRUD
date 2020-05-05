using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PDIInvoice
{
    public class PDIInvoicePartDetailListModel : BaseListWithPagingModel
   {
        public PDIInvoicePartDetailListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
        }

        public PDIInvoicePartDetailListModel()
        {

        }

        [Display(Name = "PDIInvoiceList_Display_RefNo", ResourceType = typeof(MessageResource))]
        public long RefNo { get; set; }
        [Display(Name = "PDIInvoiceList_Display_MaintenanceDate", ResourceType = typeof(MessageResource))]
        public DateTime? MaintenanceDate { get; set; }
        [Display(Name = "PDIInvoiceList_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "PDIInvoiceList_Display_PDIRecordNo", ResourceType = typeof(MessageResource))]
        public string PDIRecordNo { get; set; }
        [Display(Name = "PDIInvoiceList_Display_Quantity", ResourceType = typeof(MessageResource))]
        public long Quantity { get; set; }
        [Display(Name = "PDIInvoiceList_Display_UnitPrice", ResourceType = typeof(MessageResource))]
        public decimal? UnitPrice { get; set; }
    }
}
