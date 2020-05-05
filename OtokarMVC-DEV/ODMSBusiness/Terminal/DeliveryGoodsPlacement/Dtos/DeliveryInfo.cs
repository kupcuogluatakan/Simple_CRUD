namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    using System;
    using Common;
    public class DeliveryInfo:IResponse
    {
        public long DeliveryId { get; set; }
        public string WaybillNo { get; set; }
        public string SapNo { get; set; }
        public DateTime WaybillDate { get; set; }
        public string AcceptedBy { get; set; }
        public string Sender { get; set; }
        public bool IsCompletable { get; set; }

    }
}
