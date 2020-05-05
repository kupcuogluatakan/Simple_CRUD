using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ODMSModel.Reports
{
    public class PurchaseOrderReport
    {
        public int DealerId { get; set; }
        public int? SupplierId { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_DealerName",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_DealerRegionName",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionName { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_SupplierName",
        ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierName { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_SupplierTaxNumber",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TaxNo { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_WaybillNo",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WaybillNo { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_WaybillDate",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WaybillDate { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_PurchaseOrderNumber",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long PurchaseOrderNumber { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_DeliveryId",
ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long DeliveryId { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_InvoiceSerialNumber",
 ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceSerialNumber { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_InvoiceNumber",
      ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceNumber { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_PartCode",
       ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_PartName",
        ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_IsOriginal",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsOriginal { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_RecievedQuantity",
        ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal RecievedQuantity { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_Unit",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Unit { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_Price",
 ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Price { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_OrderPrice",
        ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal OrderPrice { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_InvoiceDate",
        ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? InvoiceDate { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_OrderDate",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? OrderDate { get; set; }
        [Display(Name = "PartStockReport_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockType { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_RecievedQuantity",
ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ShipQuant { get; set; }
    }
}
