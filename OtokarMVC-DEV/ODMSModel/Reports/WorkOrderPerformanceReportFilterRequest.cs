using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.Reports
{
    public class WorkOrderPerformanceReportFilterRequest : ReportListModelBase
    {
        public WorkOrderPerformanceReportFilterRequest(DataSourceRequest request) : base(request)
        {
        }
        public WorkOrderPerformanceReportFilterRequest()
        {

        }

        [Display(Name = "WorkOrderPartsTotalReport_Year", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Year { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_Month", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Month { get; set; }
        [Display(Name = "PartStockReport_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "PartStockReport_Region", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }
        [Display(Name = "WorkOrderPartsTotalReport_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorType { get; set; }
        [Display(Name = "GuaranteeReport_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessType { get; set; }
        [Display(Name = "GuaranteeReport_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }


        [Display(Name = "GuaranteeReport_WorkOrderNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderNo { get; set; }
        [Display(Name = "GuaranteeReport_GifNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GifNo { get; set; }
        [Display(Name = "GuaranteeReport_User", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string User { get; set; }
        [Display(Name = "GuaranteeReport_ConfirmStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ConfirmStatus { get; set; }

        [Display(Name = "GuaranteeReport_SendStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SendStatus { get; set; }
        [Display(Name = "WorkOrderCard_Dislay_VehicleLeaveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]     
        public DateTime? VehicleLeaveDate { get; set; }
    }
}
