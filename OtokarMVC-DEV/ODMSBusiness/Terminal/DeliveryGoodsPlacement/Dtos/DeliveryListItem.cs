using System;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class DeliveryListItem
    {
        public long DeliveryId { get; set; }
        public string SapDeliveryNo { get; set; }
        public string WaybillNo { get; set; }
        public string Sender { get; set; }
        public DateTime WaybillDate { get; set; }
    }
}
