using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class DeliveryInfoRequest:IRequest<DeliveryInfoRequest,DeliveryInfo>
    {
        public long DeliveryId { get; }
        public int DealerId { get; }

        public DeliveryInfoRequest(long deliveryId,int dealerId)
        {
            DeliveryId = deliveryId;
            DealerId = dealerId;
        }
    }
}
