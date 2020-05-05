using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
   public class DeliveryDetailInfoResponse:IResponse
    {
       public long DeliveryId { get; set; }
       public long DeliverySequenceNo { get; set; }
    }
}
