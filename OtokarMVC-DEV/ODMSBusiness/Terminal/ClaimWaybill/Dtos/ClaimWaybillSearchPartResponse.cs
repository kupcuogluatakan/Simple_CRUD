using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ClaimWaybillSearchPartResponse:IResponse
    {
        public long PartId { get; set; }
        public long ClaimDismantledPartId { get; set; }
    }
}
