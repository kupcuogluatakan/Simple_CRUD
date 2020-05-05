using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;

namespace ODMSModel.Reports
{
    public class WorkOrderPartsTotalReportFilterRequest : ReportListModelBase
    {
        public WorkOrderPartsTotalReportFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public WorkOrderPartsTotalReportFilterRequest()
        {
        }

        [Display(Name = "PartStockReport_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "PartStockReport_Region", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_InGuarantee", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int InGuarantee { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_Year", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Year { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_Month", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Month { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorType { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Currency { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_IsPaid", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IsPaid { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_IsOriginal", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IsOriginal { get; set; }

        [Display(Name = "PurchaseOrderReport_Display_PartCode",
 ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
    }
}
