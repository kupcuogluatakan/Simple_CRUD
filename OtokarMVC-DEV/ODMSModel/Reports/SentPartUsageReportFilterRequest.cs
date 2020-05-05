using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class SentPartUsageReportFilterRequest:ReportListModelBase
    {
        public SentPartUsageReportFilterRequest(DataSourceRequest request) : base(request)
        {
            SetMapper(null);
        }
        public SentPartUsageReportFilterRequest()
        {

        }

        /// <summary>
        /// /WO || SP
        /// </summary>
        /// 
        [Display(Name = "SaleReport_Display_SaleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SaleType { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? OrderStartDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? OrderEndDate { get; set; }
        [Required]
        [Display(Name = "WorkOrderCard_Display_Part", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long PartId { get; set; }
        [Display(Name = "Global_Display_Order_Type", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PurchaseOrderType { get; set; }
        [Display(Name = "PartStockReport_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StockTypeId { get; set; }





    }
}
