using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class DeliveryDetailListRequest:IRequest<DeliveryDetailListRequest,ListResponse<DeliveryDetailListItem>>
    {
        public string LanguageCode { get; }
        public int DealerId { get; }
        public long DeliveryId { get; }
        public GridPagingInfo PagingInfo { get; }

        public DeliveryDetailListRequest(GridPagingInfo pagingInfo, int dealerId, long deliveryId,string languageCode)
        {
            LanguageCode = languageCode;
            DealerId = dealerId;
            DeliveryId = deliveryId;
            PagingInfo = pagingInfo;
        }
    }
}
