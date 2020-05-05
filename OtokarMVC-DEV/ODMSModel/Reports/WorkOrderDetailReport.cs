using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderDetailReport
    {
        [Display(Name = "ChargePerCarReport_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "ChargePerCarReport_Region", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionName { get; set; }
        [Display(Name = "ChargePerCarReport_WorkOrder", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int WorkOrderId { get; set; }
        [Display(Name = "ChargePerCarReport_WorkOrderDetail", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int WorkOrderDetailId { get; set; }
        public string GIF { get; set; }
        [Display(Name = "ChargePerCarReport_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "ChargePerCarReport_EngineNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EngineNo { get; set; }
        [Display(Name = "ChargePerCarReport_VehicleWarrantyStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? VehicleWarrantyStartDate { get; set; }
        [Display(Name = "ChargePerCarReport_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }
        [Display(Name = "ChargePerCarReport_VehicleKm", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal VehicleKm { get; set; }
        public int DealerId { get; set; }
        public int DealerRegionId { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "ChargePerCarReport_PartPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal PartPrice { get; set; }
        [Display(Name = "ChargePerCarReport_LabourPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal LabourPrice { get; set; }
        [Display(Name = "ChargePerCarReport_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "ChargePerCarReport_PartCost", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal PartCost { get; set; }
        [Display(Name = "ChargePerCarReport_Price", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Price { get; set; }

        [Display(Name = "ChargePerCarReport_ApproveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ApproveDate { get; set; }
        [Display(Name = "ChargePerCarReport_Category", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Category { get; set; }
        [Display(Name = "ChargePerCarReport_TotalAmount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalAmount { get; set; }

    }
}
