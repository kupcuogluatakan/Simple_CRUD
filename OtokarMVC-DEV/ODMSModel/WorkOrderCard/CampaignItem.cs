
namespace ODMSModel.WorkOrderCard
{
    public class CampaignItem
    {
        public string CampaignCode { get; set; }
        public string CanpaignName { get; set; }
        public long WorkOrderDetailId { get; set; }
        public bool IsMust { get; set; }
        public bool HasStock { get; set; }
        public string Denied_Campaign_Codes { get; set; }
        public string Denied_Campaign_Service_Codes { get; set; }
    }
}
