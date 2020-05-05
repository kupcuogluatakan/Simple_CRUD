
namespace ODMSModel.WorkOrderCard
{
    public class PickingCancellationItem
    {
        public long PickingId { get; set; }
        public string StockType { get; set; }
        public long WorkOrderDetailId  { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
    }
}
