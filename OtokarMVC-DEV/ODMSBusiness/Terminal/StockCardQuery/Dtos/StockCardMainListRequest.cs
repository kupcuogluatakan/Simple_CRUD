using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockCardQuery.Dtos
{
    public class StockCardMainListRequest : IRequest<StockCardMainListRequest,EnumerableResponse<StockCardMainListItem>>
    {
        public int DealerId { get; }
        public long PartId { get; }
        public string LanguageCode { get; }

        public StockCardMainListRequest(int dealerId,long partId,string languageCode)
        {
            DealerId = dealerId;
            PartId = partId;
            LanguageCode = languageCode;
        }
    }
}
