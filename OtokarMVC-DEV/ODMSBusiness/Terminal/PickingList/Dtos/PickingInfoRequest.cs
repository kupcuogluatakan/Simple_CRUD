using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.PickingList.Handlers;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class PickingInfoRequest : IRequest<PickingInfoRequest,PickingInfo>
    {
        public long PickingId { get; }
        public int DealerId { get; }
        public string LanguageCode { get; }

        public PickingInfoRequest(long pickingId,int dealerId,string languageCode)
        {
            PickingId = pickingId;
            DealerId = dealerId;
            LanguageCode = languageCode;
        }
    }


}
