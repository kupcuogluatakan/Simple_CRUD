using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class PutawayRackSearchResponse:IResponse
    {
        public int RackId { get; set; }
        public string RackCode { get; set; }
        public decimal Quantity { get; set; }
    }
}
