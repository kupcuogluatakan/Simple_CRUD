using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;

namespace ODMSModel.Reports
{
    public class CampaignSummaryInfoRequest: ReportListModelBase
    {
        public CampaignSummaryInfoRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }
        [Display(Name = "WorkOrderDetailReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        public CampaignSummaryInfoRequest() { }
        public long DealerId { get; set; }
        public string VehicleModel { get; set; }
        public long ModelType { get; set; }
        public long RegionId { get; set; }
        public string CampaignCode { get; set; }
        public string GroupCode { get; set; }
        public int GroupType { get; set; }
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }
        public int GuaranteeStat { get; set; }
        public int IsMust { get; set; }
        public int CampaignStatus { get; set; }
        public DateTime? GuaranteeConfirmStartDate { get; set; }
        public DateTime? GuaranteeConfirmEndDate { get; set; }

        public string DealerIdList { get; set; }
        public string RegionIdList { get; set; }

    }
}
