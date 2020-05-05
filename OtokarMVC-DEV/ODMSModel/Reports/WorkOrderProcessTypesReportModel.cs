using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderProcessTypesTotalReportModel
    {
        [Display(Name = "WorkOrderProcessTypes_Group", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GroupName { get; set; }
        [Display(Name = "WorkOrderProcessTypes_TotalCarCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TotalCarCount { get; set; }
        [Display(Name = "WorkOrderProcessTypes_TotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "WorkOrderProcessTypes_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "WorkOrderProcessTypes_TotalWorkOrderCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TotalWorkOrderCount { get; set; }
        [Display(Name = "WorkOrderProcessTypes_TotalWorkOrderDetailCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TotalWorkOrderDetailCount { get; set; }
        [Display(Name = "WorkOrderProcessTypes_TotalIndicatorCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TotalIndicatorCount { get; set; }


        public List<WorkOrderProcessTypeValue> ProcessTypes { get; set; }
    }

    public class WorkOrderProcessTypeValue
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public decimal Percent { get; set; }
        public decimal TotalPercent { get; set; }
    }
}
