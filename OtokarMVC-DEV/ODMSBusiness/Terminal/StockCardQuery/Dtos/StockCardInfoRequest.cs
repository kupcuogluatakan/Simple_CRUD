using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockCardQuery.Dtos
{
    public class StockCardInfoRequest:IRequest<StockCardInfoRequest,StockCardInfo>
    {
        public string LanguageCode { get; }
        public int DealerId { get; }
        public string PartCode { get; }

        public StockCardInfoRequest(string partCode,int dealerId,string languageCode)
        {
            LanguageCode = languageCode;
            DealerId = dealerId;
            PartCode = partCode;
        }
    }
}
