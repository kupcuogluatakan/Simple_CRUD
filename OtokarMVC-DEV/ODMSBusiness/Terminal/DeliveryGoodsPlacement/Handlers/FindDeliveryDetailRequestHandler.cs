using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Handlers
{
    public class FindDeliveryDetailRequestHandler : IHandleRequest<FindDeliveryDetailRequest, DeliveryDetailInfoResponse>
    {
        private readonly DbHelper _dbHelper;

        public FindDeliveryDetailRequestHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public DeliveryDetailInfoResponse Handle(FindDeliveryDetailRequest request)
        {
            string partCode = string.Empty;
            string barCode = string.Empty;
            if (request.PartCode.Contains(";"))
            {
                var arr = request.PartCode.Split(';');
                barCode = arr[0];
                partCode = arr[1];
            }
            else
            {
                partCode = request.PartCode;
            }
            var info = _dbHelper.ExecuteReader<DeliveryDetailInfoResponse>("P_TERMINAL_GET_DELIVERY_DET_BY_PART_CODE",
                request.DeliveryId,
                Utility.MakeDbNull(barCode),
                Utility.MakeDbNull(partCode),
                request.DealerId
                );
            if (info == null || info.DeliverySequenceNo == 0)
                throw new ODMSException(MessageResource.Exception_PartNotFound);
            return info;
        }
    }
}
