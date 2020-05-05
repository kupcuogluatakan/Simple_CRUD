
namespace ODMSModel.WorkOrderCard.WarrantyDetail
{
    public class WarrantyDetailItem
    {
        public long PartId { get; set; }
        public string PartName { get; set; }
        public decimal Quantity { get; set; }
        public long RemovedPartId { get; set; }
        public string RemovedPartName { get; set; }
        public WarrantyStatus WarrantyStatus { get; set; }
    }

    public enum WarrantyStatus
    {
        Denied,
        Accepted
    }
}
