using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class PickingDetailInfoRequest:IRequest<PickingDetailInfoRequest, PickingDetailInfo>
    {
        public long PickingDetailId { get; }
        public int DealerId { get; }

        public PickingDetailInfoRequest(long pickingDetailId,int dealerId)
        {
            PickingDetailId = pickingDetailId;
            DealerId = dealerId;
        }
    }
}
