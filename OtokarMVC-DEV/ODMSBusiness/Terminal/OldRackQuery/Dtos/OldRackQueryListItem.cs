using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.OldRackQuery.Dtos
{
    public class OldRackQueryListItem
    {
        public string SourceRack { get; set; }
        public string DestinationRack { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
