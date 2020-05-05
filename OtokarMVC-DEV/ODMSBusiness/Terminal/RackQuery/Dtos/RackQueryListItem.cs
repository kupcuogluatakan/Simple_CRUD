using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.RackQuery.Dtos
{
    public class RackQueryListItem
    {
        public long PartId { get; set; }
        public string PartCode { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }
    }
}
