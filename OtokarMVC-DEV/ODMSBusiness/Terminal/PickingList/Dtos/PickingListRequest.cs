using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class PickingListRequest:IRequest<PickingListRequest,EnumerableResponse<PickingListItem>>
    {
        public int DealerId { get; }
        public GridPagingInfo PagingInfo { get; }

        public PickingListRequest(int dealerId,GridPagingInfo pagingInfo)
        {
            DealerId = dealerId;
            PagingInfo = pagingInfo;
        }
    }
}
