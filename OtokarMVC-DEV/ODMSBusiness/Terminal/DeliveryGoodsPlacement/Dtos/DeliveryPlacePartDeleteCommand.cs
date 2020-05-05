using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class DeliveryPlacePartDeleteCommand:ICommand
    {
        public long PlacementId { get; }
        public int UserId { get; }

        public DeliveryPlacePartDeleteCommand(long placementId,int userId)
        {
            PlacementId = placementId;
            UserId = userId;
        }
    }
}
