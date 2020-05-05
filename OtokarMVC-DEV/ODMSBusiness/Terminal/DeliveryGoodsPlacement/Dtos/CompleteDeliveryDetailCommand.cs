using ODMSBusiness.Terminal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class CompleteDeliveryDetailCommand : ICommand
    {

        public int DealerId { get; set; }
        public long DeliveryId { get; set; }
        public int OperatingUser { get; set; }


        public CompleteDeliveryDetailCommand(long deliveryId, int dealerId, int operatingUser)
        {
            DealerId = dealerId;
            DeliveryId = deliveryId;
            OperatingUser = operatingUser;
        }
    }
}
