using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Resources;

namespace ODMSModel.Reports
{
    public class PartStockReport
    {
        [Display(Name = "PartStockReport_Region", ResourceType = typeof(MessageResource))]
        public string DealerRegionName { get; set; }
        [Display(Name = "PartStockReport_Dealer", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "PartStockReport_Part_Code", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "PartStockReport_StockType", ResourceType = typeof(MessageResource))]
        public string StockTypeName { get; set; }
        [Display(Name = "PartStockReport_Is_Original_Part", ResourceType = typeof(MessageResource))]
        public bool IsOriginalPart { get; set; }
        [Display(Name = "PartStockReport_Part_Section", ResourceType = typeof(MessageResource))]
        public string PartSectionName { get; set; }
        [Display(Name = "PartStockReport_Part_Class", ResourceType = typeof(MessageResource))]
        public string PartClassName { get; set; }
        [Display(Name = "PartStockReport_Package_Count", ResourceType = typeof(MessageResource))]
        public decimal PackageQuantity { get; set; }
        [Display(Name = "PartStockReport_Critical_Stock", ResourceType = typeof(MessageResource))]
        public decimal CriticalStockQuantity { get; set; }
        [Display(Name = "PartStockReport_Min_Stock_Level", ResourceType = typeof(MessageResource))]
        public decimal MinStockQuantity { get; set; }
        [Display(Name = "PartStockReport_Max_Stock_Level", ResourceType = typeof(MessageResource))]
        public decimal MaxStockQunatity { get; set; }
        [Display(Name = "PartStockReport_Dealer_Startup_Stock_Level", ResourceType = typeof(MessageResource))]
        public decimal StartupQuantity { get; set; }
        [Display(Name = "PartStockReport_Cost_Avg", ResourceType = typeof(MessageResource))]
        public decimal AvgDealerPrice { get; set; }
        [Display(Name = "PartStockReport_Usage_Stock", ResourceType = typeof(MessageResource))]
        public decimal StockQuantity { get; set; }
        [Display(Name = "PartStockReport_Stock_Age", ResourceType = typeof(MessageResource))]
        public decimal StockAge { get; set; }
        [Display(Name = "PartStockReport_Part_Name", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "PartStockReport_Stock_Total_Price", ResourceType = typeof(MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "PartStockReport_Currency", ResourceType = typeof(MessageResource))]
        public string Currency { get; set; }
        public long PartId { get; set; }

        [Display(Name = "PartStockReport_TotalAmount", ResourceType = typeof(MessageResource))]
        public decimal TotalAmount { get; set; }
    }
}
