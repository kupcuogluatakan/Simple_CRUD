using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.PickingList.Handlers;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class PickingPartsRequest:IRequest<PickingPartsRequest, EnumerableResponse<PickingPartsListItem>>
    {
        public long PickingId { get; }
        public string PartCode { get; }
        public int DealerId { get; }
        public string LanguageCode { get; }

        public PickingPartsRequest(long pickingId,string partCode,int dealerId,string languageCode)
        {
            PickingId = pickingId;
            PartCode = partCode;
            DealerId = dealerId;
            LanguageCode = languageCode;
        }
    }
}
