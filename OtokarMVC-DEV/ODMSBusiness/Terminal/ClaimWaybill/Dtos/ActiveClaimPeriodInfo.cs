using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ActiveClaimPeriodInfo:IResponse
    {
        public long ClaimRecallPeriodId  { get; set; }
        public DateTime ValidLastDay { get; set; }
        public DateTime ShipFirstDay { get; set; }
        public DateTime ShipLastDay { get; set; }
        public string WaybillNo { get; set; }
        public int ClaimWaybillId { get; set; }
    }
}
