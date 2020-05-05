using System.Collections.Generic;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderCampaignModel:ModelBase
    {
        public long WorkOrderId { get; set; }
        public List<CampaignItem> Campaigns { get; set; }

        public WorkOrderCampaignModel()
        {
            Campaigns=new List<CampaignItem>();
        }
    }
}
