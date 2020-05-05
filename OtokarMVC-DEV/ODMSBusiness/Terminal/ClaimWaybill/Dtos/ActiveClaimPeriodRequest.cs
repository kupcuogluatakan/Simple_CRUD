using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ActiveClaimPeriodRequest:IRequest<ActiveClaimPeriodRequest, ActiveClaimPeriodInfo>
    {
        public int DealerId { get; }

        public ActiveClaimPeriodRequest(int dealerId)
        {
            DealerId = dealerId;
        }
    }
}
