using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.OtokarSparePartSaleInvoiceSearch
{
    public class OtokarSparePartSaleInvoiceSearchViewModel : BaseListWithPagingModel
    {
        public OtokarSparePartSaleInvoiceSearchViewModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {

        }
        public OtokarSparePartSaleInvoiceSearchViewModel()
        {

        }


        [Display(Name = "InvoiceList_Display_StartDate",
              ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "InvoiceList_Display_EndDate",
              ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "InvoiceInfoReport_Display_InvoiceNo",
              ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "SaleReport_Display_InvoiceDate",
            ResourceType = typeof(MessageResource))]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_TotalPriceWithVatRatio",
           ResourceType = typeof(MessageResource))]
        public string InvoicePriceWithVAT { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_CurrencyCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_DeliveryId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WaybillNo { get; set; }

        public int DealerId { get; set; }

    }
}
