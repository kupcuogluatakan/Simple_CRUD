using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSCommon.Exception;
using ODMSCommon.Resources;

namespace ODMSBusiness.Terminal.StockMovement.Dtos
{
    public class StockMovementCommand:ICommand
    {
        public int DealerId { get; }
        public decimal TransferQuantity { get; }
        public int FromWarehouseId { get; }
        public int FromRackId { get; }
        public long PartId { get; }
        public int ToWarehouseId { get; }
        public int ToRackId { get; }
        public int StockTypeId { get; }
        public decimal Quantity { get; }
        public string Description { get; }

        public StockMovementCommand(int dealerId,int fromWarehouseId,int fromRackId,long partId,int toWarehouseId,int toRackId, int stockTypeId,decimal quantity,string description,decimal transferQuantity)
        {
            DealerId = dealerId;
            TransferQuantity = transferQuantity;
            FromWarehouseId = fromWarehouseId;
            FromRackId = fromRackId;
            PartId = partId;
            ToWarehouseId = toWarehouseId;
            ToRackId = toRackId;
            StockTypeId = stockTypeId;
            Quantity = quantity;
            Description = description;
        }

        public void Validate()
        {
            if(ToWarehouseId<=0)
                throw new ODMSException(MessageResource.Exception_ToWareHouseMustBeSelected);
            if (ToRackId <= 0)
                throw new ODMSException(MessageResource.Exception_ToRackMustBeSelected);
            if (TransferQuantity <= 0)
                throw new ODMSException(MessageResource.Exception_TransferQuantityIsZero);
            if(TransferQuantity>Quantity)
                    throw new ODMSException(MessageResource.Exception_TransferQuantityGraterThanCurrentQuantity);

        }
    }
}
