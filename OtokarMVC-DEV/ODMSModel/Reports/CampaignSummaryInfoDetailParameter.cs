using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class CampaignSummaryInfoDetailParameter
    {
        public string CampaignCode { get; set; }
        public string GroupCode { get; set; }
        public int GroupType { get; set; }
        public string Currency { get; set; }
        public int VehicleId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DealerIdList { get; set; }
        public string RegionIdList { get; set; }
        public string VehicleModel { get; set; }
        public int GuaranteeStat { get; set; }
        public int IsMust { get; set; }
        public int CampaignStatus { get; set; }
        public DateTime? GuaranteeConfirmStartDate { get; set; }
        public DateTime? GuaranteeConfirmEndDate { get; set; }
        
    }
}
