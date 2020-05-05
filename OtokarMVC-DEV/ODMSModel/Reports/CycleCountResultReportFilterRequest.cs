using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.Reports
{
    public class CycleCountResultReportFilterRequest : ReportListModelBase
    {
        public CycleCountResultReportFilterRequest()
        {

        }
        public CycleCountResultReportFilterRequest(DataSourceRequest request) : base(request)
        {
            SetMapper(null);
        }
        [Display(Name = "CycleCountResultReport_Display_DealerIdList", ResourceType = typeof(MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "CycleCountResultReport_Display_CountBeginDate", ResourceType = typeof(MessageResource))]
        public DateTime? CountBeginDate { get; set; }
        [Display(Name = "CycleCountResultReport_Display_CountEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? CountEndDate { get; set; }
        [Display(Name = "CycleCountResultReport_Display_CountStatusList", ResourceType = typeof(MessageResource))]
        public string CountStatusList { get; set; }
        [Display(Name = "CycleCountResultReport_Display_CountApproveStartDate", ResourceType = typeof(MessageResource))]
        public DateTime? CountApproveStartDate { get; set; }
        [Display(Name = "CycleCountResultReport_Display_CountApproveEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? CountApproveEndDate { get; set; }
        [Display(Name = "CycleCountResultReport_Display_CycleCountDiffIdList", ResourceType = typeof(MessageResource))]
        public string CycleCountDiffIdList { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_IsOriginal", ResourceType = typeof(MessageResource))]
        public int? IsOriginal { get; set; }

    }
}
