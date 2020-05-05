using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockCardQuery.Dtos;

namespace ODMSBusiness.Terminal.OldRackQuery.Dtos
{
    public class OldRackQueryInfoRequest:IRequest<OldRackQueryInfoRequest, OldRackQueryInfo>
    {
        public string LanguageCode { get; }
        public int DealerId { get; }
        public string PartCode { get; }

        public OldRackQueryInfoRequest(string partCode, int dealerId, string languageCode)
        {
            LanguageCode = languageCode;
            DealerId = dealerId;
            PartCode = partCode;
        }

    }
}
