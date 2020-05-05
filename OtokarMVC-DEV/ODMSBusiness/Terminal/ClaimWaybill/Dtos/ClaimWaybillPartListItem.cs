using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ClaimWaybillPartListItem
    {
        public string PartCode { get; set; }
        public long PartId { get; set; }
        public long GifNo { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }
        public long ClaimDismantledPartId { get; set; }
    }
}
