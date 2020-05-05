using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.StockCard
{
    public class StockCardStockValueModel
    {
        public int? CurrentDealerId { get; set; }
        public int DealerId { get; set; }
        public bool IsHq { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal TrCodePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal OriginalAveragePrice { get; set; }
        public decimal TrAveragePrice { get; set; }
        public decimal AverageTotalPrice { get; set; }
        public int StockLocationId { get; set; }
        public string DealerIdsForCentral { get; set; }
        public string DealerRegionIds { get; set; }
        public int DealerRegionType { get; set; }
    }
}
