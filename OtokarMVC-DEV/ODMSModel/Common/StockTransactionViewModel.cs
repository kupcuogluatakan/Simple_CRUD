using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Common
{
    public class StockTransactionViewModel : ModelBase
    {
        public long StockTransactionId { get; set; }
        public int DealerId { get; set; }
        public int TransactionTypeId { get; set; }
        public long PartId { get; set; }
        public decimal Quantity { get; set; }
        public decimal DealerPrice { get; set; }
        public string TransactionDesc { get; set; }
        public int? FromWarehouse { get; set; }
        public int? FromRack { get; set; }
        public int? FromStockType { get; set; }
        public int? ToWarehouse { get; set; }
        public int? ToRack { get; set; }
        public int? ToStockType { get; set; }
        public decimal ReserveQnty { get; set; }
        public decimal BlockQnty { get; set; }
    }
}
