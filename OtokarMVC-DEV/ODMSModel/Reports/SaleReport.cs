using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class SaleReport
    {
        [Display(Name = "SaleReport_Display_InvoiceAmount", ResourceType = typeof(MessageResource))]
        public decimal InvoiceAmount { get; set; }
        [Display(Name = "SaleReport_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "SaleReport_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SaleReport_Display_SaleType", ResourceType = typeof(MessageResource))]
        public int SaleType { get; set; }
        [Display(Name = "SaleReport_Display_SaleType", ResourceType = typeof(MessageResource))]
        public string SaleTypeName { get; set; }
        [Display(Name = "SaleReport_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "SaleReport_Display_DealerRegionName", ResourceType = typeof(MessageResource))]
        public string DealerRegionName { get; set; }
        [Display(Name = "SaleReport_Display_PartId", ResourceType = typeof(MessageResource))]
        public long PartId { get; set; }
        [Display(Name = "SaleReport_Display_Quantity", ResourceType = typeof(MessageResource))]
        public decimal Quantity { get; set; }
        [Display(Name = "SaleReport_Display_WorkOrderId", ResourceType = typeof(MessageResource))]
        public long WorkOrderId { get; set; }
        [Display(Name = "SaleReport_Display_DiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal DiscountPrice { get; set; }
        [Display(Name = "SaleReport_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal VatRatio { get; set; }
        [Display(Name = "SaleReport_Display_DealerPrice", ResourceType = typeof(MessageResource))]
        public decimal DealerPrice { get; set; }
        [Display(Name = "SaleReport_Display_ListPrice", ResourceType = typeof(MessageResource))]
        public decimal ListPrice { get; set; }
        [Display(Name = "SaleReport_Display_Unit", ResourceType = typeof(MessageResource))]
        public string Unit { get; set; }
        [Display(Name = "SaleReport_Display_DealerTaxNo", ResourceType = typeof(MessageResource))]
        public string DealerTaxNo { get; set; }
        [Display(Name = "SaleReport_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "SaleReport_Display_CustomerAddress", ResourceType = typeof(MessageResource))]
        public string CustomerAddress { get; set; }
        [Display(Name = "SaleReport_Display_CustomerTaxOffice", ResourceType = typeof(MessageResource))]
        public string CustomerTaxOffice { get; set; }
        [Display(Name = "SaleReport_Display_InvoiceDate", ResourceType = typeof(MessageResource))]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "SaleReport_Display_InvoiceSerialNo", ResourceType = typeof(MessageResource))]
        public string InvoiceSerialNo { get; set; }
        [Display(Name = "SaleReport_Display_InvoiceNo", ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "SaleReport_Display_DeliveryDate", ResourceType = typeof(MessageResource))]
        public DateTime DeliveryDate { get; set; }
        [Display(Name = "SaleReport_Display_DeliveryHour", ResourceType = typeof(MessageResource))]
        public string DeliveryHour { get; set; }
        [Display(Name = "SaleReport_Display_WitholdRatio", ResourceType = typeof(MessageResource))]
        public string WitholdRatio { get; set; }
        [Display(Name = "SaleReport_Display_WitholdAmount", ResourceType = typeof(MessageResource))]
        public decimal WitholdAmount { get; set; }
        [Display(Name = "SaleReport_Display_CustomerId", ResourceType = typeof(MessageResource))]
        public int CustomerId { get; set; }
        [Display(Name = "SaleReport_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }
        [Display(Name = "SaleReport_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "SaleReport_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "SaleReport_Display_VehicleCode", ResourceType = typeof(MessageResource))]
        public string VehicleCode { get; set; }
        [Display(Name = "SaleReport_Display_VehicleModel", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "SaleReport_Display_VehicleType", ResourceType = typeof(MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "SaleReport_Display_WayBillNo", ResourceType = typeof(MessageResource))]
        public string WayBillNo { get; set; }
        [Display(Name = "SaleReport_Display_WayBillDate", ResourceType = typeof(MessageResource))]
        public DateTime? WayBillDate { get; set; }
        [Display(Name = "SaleReport_Display_OriginalPartCode", ResourceType = typeof(MessageResource))]
        public string OriginalPartCode { get; set; }
        [Display(Name = "SaleReport_Display_OriginalPartName", ResourceType = typeof(MessageResource))]
        public string OriginalPartName { get; set; }
        [Display(Name = "SaleReport_Display_DiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal DiscountRatio { get; set; }
        [Display(Name = "SaleReport_Display_RecordDate", ResourceType = typeof(MessageResource))]
        public DateTime? RecordDate { get; set; }
        [Display(Name = "SaleReport_Display_VatPrice", ResourceType = typeof(MessageResource))]
        public decimal VatPrice { get; set; }
        [Display(Name = "SaleReport_Display_VehicleLeaveDate", ResourceType = typeof(MessageResource))]
        public DateTime? VehicleLeaveDate { get; set; }
        [Display(Name = "SaleReport_Display_InvoiceUpdateDate", ResourceType = typeof(MessageResource))]
        public DateTime? InvoiceUpdateDate { get; set; }
        public string WithOld { get; set; }
        public decimal? WithOldPrice { get; set; }
        [Display(Name = "SaleReport_Display_StockAmount", ResourceType = typeof(MessageResource))]
        public decimal? StockAmount { get; set; }
        [Display(Name = "SaleReport_Display_TotalDealerPrice", ResourceType = typeof(MessageResource))]
        public decimal? TotalDealerPrice { get; set; }
        [Display(Name = "SaleReport_Display_Invoiced", ResourceType = typeof(MessageResource))]
        public bool Invoiced { get; set; }
        [Display(Name = "SaleReport_Display_DiscountedPrice", ResourceType = typeof(MessageResource))]
        public decimal DiscountedPrice { get { return ListPrice - DiscountPrice; } }
    }
}
