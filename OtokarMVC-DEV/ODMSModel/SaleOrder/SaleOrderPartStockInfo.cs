using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.SaleOrder
{
    public class SaleOrderPartStockInfo
    {
        public long PartId { get; set; }
        public long SoDetSeqNo { get; set; }
        public decimal StockQuantity { get; set; }
    }
}
