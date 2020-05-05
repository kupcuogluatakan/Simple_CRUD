using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.RackQuery.Dtos
{
    public class RackQueryListRequest:IRequest<RackQueryListRequest,EnumerableResponse<RackQueryListItem>>
    {
        public string LanguageCode { get; }
        public int DealerId { get; }
        public int WarehouseId { get; }
        public int RackId { get; }

        public RackQueryListRequest(int dealerId,int warehouseId,int rackId,string languageCode)
        {
            LanguageCode = languageCode;
            DealerId = dealerId;
            WarehouseId = warehouseId;
            RackId = rackId;
        }
    }
}
