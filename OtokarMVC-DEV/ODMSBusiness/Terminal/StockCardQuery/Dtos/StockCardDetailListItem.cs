using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.StockCardQuery.Dtos
{
    public class StockCardDetailListItem
    {
        public string WarehouseName { get; set; }
        public string RackName { get; set; }
        public decimal Quantity { get; set; }
    }
}
