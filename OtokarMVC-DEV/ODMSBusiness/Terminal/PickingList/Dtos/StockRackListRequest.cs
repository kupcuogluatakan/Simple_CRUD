using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class StockRackListRequest:IRequest<StockRackListRequest,EnumerableResponse<StockRackListItem>>
    {
        public string RackCode { get; }
        public int DealerId { get; }
        public long PickingDetailId { get; }

        public StockRackListRequest(long pickingDetailId,int dealerId,string rackCode)
        {
            RackCode = rackCode;
            DealerId = dealerId;
            PickingDetailId = pickingDetailId;
        }
    }
}
