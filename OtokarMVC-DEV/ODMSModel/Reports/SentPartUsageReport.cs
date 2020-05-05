using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class SentPartUsageReport
    {
        
        [Display(Name = "SentPartUsageReport_Display_PoNumber", ResourceType = typeof(MessageResource))]
        public long PoNumber { get; set; }
        [Display(Name = "SentPartUsageReport_Display_PartId", ResourceType = typeof(MessageResource))]
        public long PartId { get; set; }
        [Display(Name = "SentPartUsageReport_Display_PurchaseOrderType", ResourceType = typeof(MessageResource))]
        public string PurchaseOrderType { get; set; }
        [Display(Name = "SentPartUsageReport_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "SentPartUsageReport_Display_ActualVinNo", ResourceType = typeof(MessageResource))]
        public string ActualVinNo { get; set; }
        [Display(Name = "SentPartUsageReport_Display_OrderDate", ResourceType = typeof(MessageResource))]
        public DateTime OrderDate { get; set; }
        [Display(Name = "SentPartUsageReport_Display_PlacementDate", ResourceType = typeof(MessageResource))]
        public DateTime PlacementDate { get; set; }
        [Display(Name = "SentPartUsageReport_Display_OrderQuantity", ResourceType = typeof(MessageResource))]
        public decimal OrderQuantity { get; set; }
        [Display(Name = "SentPartUsageReport_Display_VehicleUseDate", ResourceType = typeof(MessageResource))]
        public DateTime VehicleUseDate { get; set; }
        [Display(Name = "SentPartUsageReport_Display_WorkOrderCloseDate", ResourceType = typeof(MessageResource))]
        public DateTime WorkOrderCloseDate { get; set; }
        [Display(Name = "VehicleHistory_Display_WorkOrderDate", ResourceType = typeof(MessageResource))]
        public DateTime WorkOrderOpenDate { get; set; }
        [Display(Name = "StockBlock_Display_Dealer", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "ClaimDismantledPartDelivery_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "ClaimDismantledPartDelivery_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePartSale_Display_WayBillDate", ResourceType = typeof(MessageResource))]
        public DateTime WaybillDate { get; set; }
        [Display(Name = "Delivery_Display_ShipQuantity", ResourceType = typeof(MessageResource))]
        public decimal ShippedQuantity { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_WorkOrderNo",
    ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderNo { get; set; }

        [Display(Name = "SentPartUsageReport_Display_Interval", ResourceType = typeof(MessageResource))]
        public long IntervalDate { get; set; }
    }
}
