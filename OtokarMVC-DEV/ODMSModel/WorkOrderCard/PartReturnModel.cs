
namespace ODMSModel.WorkOrderCard
{
    public class PartReturnModel:PartReservationInfo
    {
        public string PartId { get; set; }
        public new string PartCode { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public decimal PickedQuantity { get; set; }
    }
}
