using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.OldRackQuery.Dtos
{
    public class OldRackQueryListRequest:IRequest<OldRackQueryListRequest,EnumerableResponse<OldRackQueryListItem>>
    {
        public long PartId { get; }
        public int DealerId { get; }

        public OldRackQueryListRequest(long partId,int dealerId)
        {
            PartId = partId;
            DealerId = dealerId;
        }
    }
}
