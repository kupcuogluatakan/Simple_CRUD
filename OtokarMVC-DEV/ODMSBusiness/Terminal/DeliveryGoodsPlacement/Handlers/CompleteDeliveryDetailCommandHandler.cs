using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSData.Utility;
using System;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Handlers
{
    public class CompleteDeliveryDetailCommandHandler : IHandleCommand<CompleteDeliveryDetailCommand>
    {
        private readonly DbHelper _dbHelper;
        public CompleteDeliveryDetailCommandHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public void Handle(CompleteDeliveryDetailCommand request)
        {
            var info = _dbHelper.ExecuteNonQuery<CompleteDeliveryDetailCommand>("P_COMPLETE_DELIVERY_GOODS_PLACEMENT",
                request.DeliveryId,
                request.DealerId,
                request.OperatingUser,
                Utility.MakeDbNull(null),
                Utility.MakeDbNull(null)
                );
            var errorNo = Convert.ToInt32(_dbHelper.GetOutputValue("ERROR_NO"));
            if (errorNo > 0)
                throw new ODMSException(Utility.ResolveDatabaseErrorXml(_dbHelper.GetOutputValue("ERROR_DESC").ToString()));
        }
    }
}
