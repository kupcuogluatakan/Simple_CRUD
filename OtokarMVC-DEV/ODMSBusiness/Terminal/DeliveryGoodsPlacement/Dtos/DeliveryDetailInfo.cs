namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    using ODMSBusiness.Terminal.Common;

    public class DeliveryDetailInfo: IResponse
    {
        public long DeliverySequenceNo { get; set; }
        public string PartName { get; set; }
        public decimal AcceptedAmount { get; set; }
        public decimal LeftAmount { get; set; }
        public long PartId { get; set; }
    }
}
