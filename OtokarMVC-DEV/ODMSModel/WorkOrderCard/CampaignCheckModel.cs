
namespace ODMSModel.WorkOrderCard
{
    public class CampaignCheckModel
    {
        public string CampaignCode { get; set; }
        public string Description { get; set; }
        public bool IsMust { get; set; }
        public bool HasStock { get; set; }
        public decimal TotalLabourDuration { get; set; }
    }
}
