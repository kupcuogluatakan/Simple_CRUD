using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class PutawayRackSearchRequest:IRequest<PutawayRackSearchRequest, PutawayRackSearchResponse>
    {
        public int WarehouseId { get; }
        public string RackCode { get; }
        public long PartId { get; }
        public int DealerId { get; }

        public PutawayRackSearchRequest(string rackCode,int warehouseId,long partId,int dealerId)
        {
            WarehouseId = warehouseId;
            RackCode = rackCode;
            PartId = partId;
            DealerId = dealerId;
        }
    }
}
