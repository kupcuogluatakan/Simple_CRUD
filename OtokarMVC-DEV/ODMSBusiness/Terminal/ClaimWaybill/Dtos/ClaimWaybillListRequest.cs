using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ClaimWaybillListRequest:IRequest<ClaimWaybillListRequest,EnumerableResponse<ClaimWaybillListItem>>
    {
        public int DealerId { get; }

        public ClaimWaybillListRequest(int dealerId)
        {
            DealerId = dealerId;
        }
    }
}
