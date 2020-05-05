using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.Reports
{
    public class WorkOrderPartHistoryReportFilterRequest : ReportListModelBase
    {
        public WorkOrderPartHistoryReportFilterRequest(DataSourceRequest request) : base(request)
        {
            SetMapper(null);
        }
        public WorkOrderPartHistoryReportFilterRequest()
        {

        }
        [Display(Name = "WorkOrderPartHistoryReport_Display_DealerIdList", ResourceType = typeof(MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_BeginDate", ResourceType = typeof(MessageResource))]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "WorkOrderPartHistoryReport_Display_StockTypeList", ResourceType = typeof(MessageResource))]
        public string StockTypeList { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_WorkOrderCardStatList", ResourceType = typeof(MessageResource))]
        public string WorkOrderCardStatList { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_IndicatorList", ResourceType = typeof(MessageResource))]
        public string IndicatorList { get; set; }
    }
}
