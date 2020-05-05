using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
   public class ProposalCampaignItem
    {
        public string CampaignCode { get; set; }
        public string CanpaignName { get; set; }
        public long ProposalDetailId { get; set; }
        public bool IsMust { get; set; }
        public bool HasStock { get; set; }
    }
}
