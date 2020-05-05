using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.Reports
{
    public class PartInfoModel
    {
        [Display(Name= "WorkOrderBatchInvoice_Display_WorkOrderId", ResourceType = typeof(MessageResource))]
        public long WorkOrderId { get; set; }
        [Display(Name = "VehicleHistory_Display_WorkOrderDate", ResourceType = typeof(MessageResource))]
        public DateTime WorkOrderDate { get; set; }
        [Display(Name = "PartExchangeReport_Display_TotalPrice", ResourceType = typeof(MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "PartExchangeReport_Display_TotalQuantity", ResourceType = typeof(MessageResource))]
        public decimal TotalQuantity { get; set; }
        [Display(Name = "VehicleHistory_Display_VehicleKM", ResourceType = typeof(MessageResource))]
        public string VehicleKilometer { get; set; }
        [Display(Name = "Appointment_Display_Dealer", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "CampaignPart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "ClaimDismantledPartDelivery_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "PartExchangeReport_Display_Currency", ResourceType = typeof(MessageResource))]
        public string Currency { get; set; }
        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "Appointment_Display_VehicleType", ResourceType = typeof(MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "Appointment_Display_Customer", ResourceType = typeof(MessageResource))]
        public string Customer { get; set; }
        [Display(Name = "Vehicle_Display_EngineNo", ResourceType = typeof(MessageResource))]
        public string EngineNo { get; set; }
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "PartExchangeReport_Display_DealerRegionName", ResourceType = typeof(MessageResource))]
        public string DealerRegionName { get; set; }

    }
}
