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
    public class DeliveryDetailInfoHandler:IHandleRequest<DeliveryDetailInfoRequest,DeliveryDetailInfo>
    {
        private readonly DbHelper _dbHelper;

        public DeliveryDetailInfoHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DeliveryDetailInfo Handle(DeliveryDetailInfoRequest request)
        {
            return _dbHelper.ExecuteReader<DeliveryDetailInfo>("PT_GET_DELIVERY_DETAIL_INFO",
              request.DealerId,
              request.SeqNo
              );
        }
    }
}
