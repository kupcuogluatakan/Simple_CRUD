using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ClaimPartListApprove
{
    public class ClaimDismantledPartInfoModel
    {
        public long WorkOrderId { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public decimal Quantity { get; set; }
        public string DealerShortName { get; set; }
        public string DismantledPartSerialNo { get; set; }
    }
}
