using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ClaimWaybillSearchPartRequest:IRequest<ClaimWaybillSearchPartRequest, ClaimWaybillSearchPartResponse>
    {
        public int ClaimWaybillId { get; }
        public string PartCode { get; }
        public int DealerId { get; }

        public ClaimWaybillSearchPartRequest(int claimWaybillId,string partCode,int dealerId)
        {
            ClaimWaybillId = claimWaybillId;
            PartCode = partCode;
            DealerId = dealerId;
        }
    }
}
