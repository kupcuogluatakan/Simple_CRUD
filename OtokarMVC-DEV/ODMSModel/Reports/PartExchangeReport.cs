using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class PartExchangeReport
    {
        //public int DealerRegionId { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "PartExchangeReport_Display_DealerRegionName", ResourceType = typeof(MessageResource))]
        public string DealerRegionName { get; set; }
        [Display(Name = "PartExchangeReport_Display_VehicleModel", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "PartExchangeReport_Display_DealerId", ResourceType = typeof(MessageResource))]
        public string DealerId { get; set; }
        public string DealerRegionId { get; set; }
        public string ProcessType { get; set; }
        public string Category { get; set; }

        [Display(Name = "Appointment_Display_Dealer", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "PartExchangeReport_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "PartExchangeReport_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "PartExchangeReport_Display_TotalPrice", ResourceType = typeof(MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "PartExchangeReport_Display_TotalQuantity", ResourceType = typeof(MessageResource))]
        public decimal TotalQuantity { get; set; }
        [Display(Name = "PartExchangeReport_Display_FreePartQuantity", ResourceType = typeof(MessageResource))]
        public decimal FreePartQuantity { get; set; }
        [Display(Name = "PartExchangeReport_Display_NonFreePartQuantity", ResourceType = typeof(MessageResource))]
        public decimal NonFreePartQuantity { get; set; }
        [Display(Name = "PartExchangeReport_Display_Currency", ResourceType = typeof(MessageResource))]
        public string Currency { get; set; }
        [Display(Name = "PartExchangeReport_Display_MaxKM", ResourceType = typeof(MessageResource))]
        public string MaxKM { get; set; }
        [Display(Name = "PartExchangeReport_Display_MinKM", ResourceType = typeof(MessageResource))]
        public string MinKM { get; set; }
        [Display(Name = "PartExchangeReport_Display_AverageKM", ResourceType = typeof(MessageResource))]
        public string AverageKM { get; set; }
        public long PartId { get; set; }
        [Display(Name = "PartExchangeReport_Display_CampaignPrice", ResourceType = typeof(MessageResource))]
        public decimal CampaignPrice { get; set; }
        [Display(Name = "PartExchangeReport_Display_AvgPrice", ResourceType = typeof(MessageResource))]
        public decimal AvgPrice { get; set; }
    }
}
