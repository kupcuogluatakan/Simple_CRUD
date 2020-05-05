using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ClaimWaybillResponse:IResponse
    {
        public int ClaimWaybillId { get; }
        public long ClaimPeriodId { get; }
        public ClaimWaybillResponse(long claimPeriodId,int claimWaybillId)
        {
            ClaimWaybillId = claimWaybillId;
            ClaimPeriodId = claimPeriodId;
        }
    }
}
