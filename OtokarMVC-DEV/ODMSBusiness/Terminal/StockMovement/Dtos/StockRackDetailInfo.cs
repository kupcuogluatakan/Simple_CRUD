using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockMovement.Dtos
{
    public class StockRackDetailInfo:IResponse
    {
        public long PartId { get; set; }
        public string PartCode { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }
    }
}
