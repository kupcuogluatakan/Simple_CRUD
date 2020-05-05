using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class FindDeliveryDetailRequest:IRequest<FindDeliveryDetailRequest, DeliveryDetailInfoResponse>
    {
        public int DealerId { get; }
        public long DeliveryId { get; }
        public string PartCode { get; }

        public FindDeliveryDetailRequest(long deliveryId,string partCode,int dealerId)
        {
            DealerId = dealerId;
            DeliveryId = deliveryId;
            PartCode = partCode;
        }
    }
}
