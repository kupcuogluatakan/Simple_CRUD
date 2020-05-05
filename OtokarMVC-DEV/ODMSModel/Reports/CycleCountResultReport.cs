using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Resources;

namespace ODMSModel.Reports
{
    public class CycleCountResultReport
    {

        [Display(Name= "CycleCountResultReport_Display_WareHouseName", ResourceType = typeof(MessageResource))]
        public string WareHouseName { get; set; }
        [Display(Name = "CycleCountResultReport_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "CycleCountResultReport_Display_RackName", ResourceType = typeof(MessageResource))]
        public string RackName { get; set; }
        [Display(Name = "CycleCountResultReport_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "CycleCountResultReport_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "CycleCountResultReport_Display_Unit", ResourceType = typeof(MessageResource))]
        public string Unit { get; set; }
        [Display(Name = "CycleCountResultReport_Display_BeforeCountQuantity", ResourceType = typeof(MessageResource))]
        public decimal BeforeCountQuantity { get; set; }
        [Display(Name = "CycleCountResultReport_Display_AfterCountQuantity", ResourceType = typeof(MessageResource))]
        public decimal AfterCountQuantity { get; set; }
        [Display(Name = "CycleCountResultReport_Display_ApprovedCountQuantity", ResourceType = typeof(MessageResource))]
        public decimal ApprovedCountQuantity { get; set; }
        [Display(Name = "CycleCountResultReport_Display_StockDifference", ResourceType = typeof(MessageResource))]
        public decimal StockDifference { get; set; }
        [Display(Name = "CycleCountResultReport_Display_Cost", ResourceType = typeof(MessageResource))]
        public decimal Cost { get; set; }
        [Display(Name = "CycleCountResultReport_Display_UnitCost", ResourceType = typeof(MessageResource))]
        public decimal UnitCost { get; set; }
        [Display(Name = "CycleCountResultReport_Display_CycleName", ResourceType = typeof(MessageResource))]
        public string CycleName { get; set; }
        [Display(Name = "CycleCountResultReport_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime StartDate { get; set; }
    }
}
