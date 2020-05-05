using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class SparePartControlReport
    {
        [Display(Name = "PartStockReport_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "PartStockReport_Region", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionName { get; set; }
        [Display(Name = "PartStockReport_Part_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "PartStockReport_Part_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "PartStockReport_UsableQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal UsableQuantity { get; set; }
        [Display(Name = "PartStockReport_ReserveQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ReserveQuantity { get; set; }
        [Display(Name = "PartStockReport_BlockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BlockQuantity { get; set; }
        [Display(Name = "PartStockReport_OpenOrderCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int OpenOrderCount { get; set; }
        [Display(Name = "PartStockReport_LastOpenOrderDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime LastOpenOrderDate { get; set; }
        [Display(Name = "PartStockReport_TotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "PartStockReport_CurrencyCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }
    }
}
