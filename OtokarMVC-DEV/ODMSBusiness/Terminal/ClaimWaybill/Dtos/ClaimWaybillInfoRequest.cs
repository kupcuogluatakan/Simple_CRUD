using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ClaimWaybillInfoRequest:IRequest<ClaimWaybillInfoRequest,ClaimWaybillInfo>
    {
        public long ClaimPeriodId { get; }
        public int ClaimWaybillId { get; }
        public int DealerId { get; }

        public ClaimWaybillInfoRequest(long claimPeriodId,int claimWaybillId,int dealerId)
        {
            ClaimPeriodId = claimPeriodId;
            ClaimWaybillId = claimWaybillId;
            DealerId = dealerId;
        }
    }
}
