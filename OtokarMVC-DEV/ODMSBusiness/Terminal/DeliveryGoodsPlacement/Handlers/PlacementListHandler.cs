using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;
using ODMSCommon.Security;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Handlers
{
    public class PlacementListHandler:IHandleRequest<PartsPlacementListRequest,ListResponse<PartsPlacementListItem>>
    {
        private readonly DeliveryGoodsPlacementBL _bus;

        public PlacementListHandler(DeliveryGoodsPlacementBL bus)
        {
            _bus = bus;
        }

        public ListResponse<PartsPlacementListItem> Handle(PartsPlacementListRequest request)
        {
            var response = new ListResponse<PartsPlacementListItem>();
            var total = 0;
            var list = _bus.ListPartsPlacement(UserManager.UserInfo, request, out total).Data;
            response.Items = list.Select(c=> new PartsPlacementListItem
            {
                DeliveryId = c.DeliveryId,
                DeliverySeqNo = c.DeliverySeqNo,
                PlacementId = c.PlacementId,
                Quantity = c.Quantity,
                Text = c.Text,
                Value = c.Value
            }).ToList();
            response.PageInfo = new GridPagingInfo {CurrentPage = 1, Total = total};
            return response;
        }
    }
}
