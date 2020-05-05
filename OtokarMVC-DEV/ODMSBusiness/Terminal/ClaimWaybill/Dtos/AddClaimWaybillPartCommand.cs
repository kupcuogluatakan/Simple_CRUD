using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class AddClaimWaybillPartCommand:ICommand
    {
        public int ClaimWaybillId { get; }
        public long ClaimDismantledPartId { get; }
        public int UserId { get; }

        public AddClaimWaybillPartCommand(int claimWaybillId,long claimDismantledPartId,int userId)
        {
            ClaimWaybillId = claimWaybillId;
            ClaimDismantledPartId = claimDismantledPartId;
            UserId = userId;
        }
    }
}
