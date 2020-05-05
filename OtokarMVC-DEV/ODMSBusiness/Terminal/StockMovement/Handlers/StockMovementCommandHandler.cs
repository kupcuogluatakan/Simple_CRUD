using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockMovement.Dtos;
using ODMSCommon;
using ODMSCommon.Exception;
using ODMSCommon.Security;
using ODMSModel.StockRackDetail;

namespace ODMSBusiness.Terminal.StockMovement.Handlers
{
    public class StockMovementCommandHandler:IHandleCommand<StockMovementCommand>
    {
        private readonly StockRackDetailBL _bus;

        public StockMovementCommandHandler(StockRackDetailBL bus)
        {
            _bus = bus;
        }

        public void Handle(StockMovementCommand command)
        {
            command.Validate();
            var model = MapStockExchangeModel(command);
            _bus.DMLStockExchange(UserManager.UserInfo, model);
            if (model.ErrorNo > 0)
            {
                model.ErrorMessage = Utility.ResolveDatabaseErrorXml(model.ErrorMessage);
                throw new ODMSException(model.ErrorMessage);
            }

        }

        public StockExchangeViewModel MapStockExchangeModel(StockMovementCommand command )
        {
         return new StockExchangeViewModel
         {
             FromRackId = command.FromRackId,
             FromWarehouseId = command.FromWarehouseId,
             CommandType = CommonValues.DMLType.Insert,
             PartId = command.PartId.GetValue<int?>(),
             Quantity = command.TransferQuantity,
             StockTypeId = command.StockTypeId,
             ToRackId = command.ToRackId,
             ToWarehouseId = command.ToWarehouseId,
             Description = command.Description
         };   
        }

    }
}
