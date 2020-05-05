using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ClaimWaybillPartListRequest:IRequest<ClaimWaybillPartListRequest,EnumerableResponse<ClaimWaybillPartListItem>>
    {
        public string LanguageCode { get; }
        public int ClaimWaybillId { get; }
        public int DealerId { get; }

        public ClaimWaybillPartListRequest(int claimWaybillId,int dealerId,string languageCode)
        {
            LanguageCode = languageCode;
            ClaimWaybillId = claimWaybillId;
            DealerId = dealerId;
        }
    }
}
