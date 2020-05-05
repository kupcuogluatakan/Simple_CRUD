using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class DeliveryDetailInfoRequest:IRequest<DeliveryDetailInfoRequest, DeliveryDetailInfo>
    {
        public long SeqNo { get; }
        public int DealerId { get; }

        public DeliveryDetailInfoRequest(long seqNo,int dealerId)
        {
            SeqNo = seqNo;
            DealerId = dealerId;
        }
    }
}
