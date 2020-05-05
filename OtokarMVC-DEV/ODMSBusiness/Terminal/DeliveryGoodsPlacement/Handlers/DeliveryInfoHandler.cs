using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Handlers
{
    public class DeliveryInfoHandler:IHandleRequest<DeliveryInfoRequest,DeliveryInfo>
    {
        private readonly DbHelper _dbHelper;

        public DeliveryInfoHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DeliveryInfo Handle(DeliveryInfoRequest request)
        {
            return _dbHelper.ExecuteReader<DeliveryInfo>("PT_GET_DELIVERY_INFO",
               request.DealerId,
               request.DeliveryId
               );
        }
    }
}
