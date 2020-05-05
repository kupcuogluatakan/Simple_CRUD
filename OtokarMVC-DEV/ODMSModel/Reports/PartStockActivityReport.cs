using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class PartStockActivityReport
    {
        [Display(Name = "PartStockActivityFilterRequest_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessTypeName { get; set; }

        public int ProcessType { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_Month", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int MonthIndex { get; set; }
        [Display(Name = "PartExchangeReport_Display_TotalQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Total { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M1", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M1 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M2", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M2 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M3", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M3 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M4", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M4 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M5", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M5 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M6", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M6 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M7", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M7 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M8", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M8 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M9", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M9 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M10", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M10 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M11", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M11 { get; set; }
        [Display(Name = "PartStockActivityFilterRequest_M12", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? M12 { get; set; }
    }
}
