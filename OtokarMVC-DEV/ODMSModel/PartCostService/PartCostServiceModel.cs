using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.PartCostService
{
    public class PartCostServiceModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PartCode { get; set; }
        public DateTime ApproveDate { get; set; }
        public int StockType { get; set; }
        public string PartId { get; set; }
        public string GuaranteeId { get; set; }
        public string GuaranteeSeq { get; set; }
        public decimal Avg_Cost { get; set; }
        public string WorkOrderDetailId { get; set; }
    }
}
