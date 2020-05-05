using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PDIInvoice
{
    public class PDIInvoiceListModel : BaseListWithPagingModel
   {
        public PDIInvoiceListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
        }

        public PDIInvoiceListModel()
        {

        }
        public int DealerId { get; set; }

        [Display(Name = "PDIInvoiceList_Display_RefNo", ResourceType = typeof(MessageResource))]
        public long RefNo { get; set; }
        [Display(Name = "PDIInvoiceList_Display_Type", ResourceType = typeof(MessageResource))]
        public string Type { get; set; }
        [Display(Name = "PDIInvoiceList_Display_OperationDateTime", ResourceType = typeof(MessageResource))]
        public DateTime? OperationDateTime { get; set; }
        [Display(Name = "PDIInvoiceList_Display_TotalLabourPrice", ResourceType = typeof(MessageResource))]
        public decimal? TotalLabourPrice { get; set; }
        [Display(Name = "PDIInvoiceList_Display_TotalPartPrice", ResourceType = typeof(MessageResource))]
        public decimal? TotalPartPrice { get; set; }
        [Display(Name = "PDIInvoiceList_Display_TotalMaintenancePrice", ResourceType = typeof(MessageResource))]
        public decimal? TotalMaintenancePrice { get; set; }
        [Display(Name = "PDIInvoiceList_Display_TotalPDIPrice", ResourceType = typeof(MessageResource))]
        public decimal? TotalPDIPrice { get; set; }
        [Display(Name = "PDIInvoiceList_Display_VehicleGroupCode", ResourceType = typeof(MessageResource))]
        public string VehicleGroupCode { get; set; }
    }
}
