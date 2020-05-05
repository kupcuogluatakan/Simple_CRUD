namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class DeliveryDetailListItem
    {
        public long DeliverySequenceNo { get; set; }
        public string PartName { get; set; }
        public decimal AcceptedAmount { get; set; }
        public string Type { get; set; }
        public decimal LeftAmount { get; set; }
    }
}
