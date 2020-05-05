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
    public class DeliveryDetailListHandler:IHandleRequest<DeliveryDetailListRequest,ListResponse<DeliveryDetailListItem>>
    {
        private readonly DbHelper _dbHelper;

        public DeliveryDetailListHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public ListResponse<DeliveryDetailListItem> Handle(DeliveryDetailListRequest request)
        {
            var response = new ListResponse<DeliveryDetailListItem>();
            response.Items = _dbHelper.ExecuteListReader<DeliveryDetailListItem>("PT_LIST_DELIVERY_DETAILS",
                request.DealerId,
                Utility.MakeDbNull(request.DeliveryId),
                request.LanguageCode,
                request.PagingInfo.CurrentPage,
                request.PagingInfo.PageSize,
                0
                );
            request.PagingInfo.Total = int.Parse(_dbHelper.GetOutputValue("TOTAL").ToString());
            response.PageInfo = request.PagingInfo;
            return response;
        }
    }
}
