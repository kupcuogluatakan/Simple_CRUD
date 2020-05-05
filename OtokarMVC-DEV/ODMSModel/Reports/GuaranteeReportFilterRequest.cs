using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class GuaranteeReportFilterRequest : ReportListModelBase
    {
        public GuaranteeReportFilterRequest([DataSourceRequest] DataSourceRequest request) : base(request)
        { SetMapper(null); }
        public GuaranteeReportFilterRequest()
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
        [Display(Name = "GuaranteeReport_GuaranteeCategory", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GuaranteeCategory { get; set; }
        [Display(Name = "GuaranteeReport_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

         [Display(Name = "GuaranteeReport_ConfirmedUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ConfirmedUser { get; set; }
    }
}
