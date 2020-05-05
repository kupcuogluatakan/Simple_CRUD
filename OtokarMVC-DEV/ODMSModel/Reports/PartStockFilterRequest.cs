using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.Reports
{
    public class PartStockFilterRequest : ReportListModelBase
    {
        public PartStockFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public PartStockFilterRequest() { }
        [Display(Name = "PartStockReport_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeIdList { get; set; }
        [Display(Name = "PartStockReport_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "PartStockReport_Region", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }
        [Display(Name = "PartStockReport_Is_Original_Part", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IsOriginal { get; set; }
        [Display(Name = "PartStockReport_Date", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? Date { get; set; }
        [Display(Name = "PartStockReport_Part_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartId { get; set; }
        [Display(Name = "PartStockReport_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Currency { get; set; }
        [Display(Name = "PartStockReport_Part_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "PartStockReport_Part_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCodeList { get; set; }
        [Display(Name = "PartStockReport_Part_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "PartStockReport_Part_Class", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartClassCodes { get; set; }

        [Display(Name = "PartStockReport_VehicleIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleGroupIdList { get; set; }
    }
}
