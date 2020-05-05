using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.PartCostPriceService
{
    public class PartCostPriceXMLModel
    {
        public DateTime Date { get; set; }
        public string PartCode { get; set; }
        public decimal AvgCost { get; set; }
        public string StockType { get; set; }
    }
}
