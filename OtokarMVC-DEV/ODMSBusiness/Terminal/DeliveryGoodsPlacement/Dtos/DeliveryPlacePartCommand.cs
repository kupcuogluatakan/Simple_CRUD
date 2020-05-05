using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSCommon.Exception;
using ODMSCommon.Resources;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class DeliveryPlacePartCommand:ICommand
    {
        public decimal LeftQuantity { get; }
        public long DeliverySeqNo { get; }
        public int RackId { get; }
        public decimal Quantity { get; }
        public int UserId { get; }

        public DeliveryPlacePartCommand(long deliverySeqNo,int rackId,decimal quantity,decimal leftQuantity,int userId)
        {
            LeftQuantity = leftQuantity;
            if(rackId == 0)
                throw new ODMSException(MessageResource.Exception_RackNotFound);
            if (quantity <= 0)
                throw new ODMSException(MessageResource.Exception_InvalidQuantity);
            if(quantity>leftQuantity)
                throw new ODMSException(MessageResource.Exception_QuantityGraterThanLeftQuantity);

            DeliverySeqNo = deliverySeqNo;
            RackId = rackId;
            Quantity = quantity;
            UserId = userId;
        }
    }
}
