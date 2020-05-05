using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalCampaignModel : ModelBase
    {
        public ProposalCampaignModel()
        {
            Campaigns = new List<ProposalCampaignItem>();
        }
        public long ProposalId { get; set; }
        public List<ProposalCampaignItem> Campaigns { get; set; }
    }
}
