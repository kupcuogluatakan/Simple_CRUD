using ODMSBusiness.Terminal.Common;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class DeliveryGoodsPlacementBL:BaseBusiness
    {
        private readonly DbHelper _dbHelper = new DbHelper(makeParamsDbNull: false);

        public ListResponse<DeliveryListItem> ListDeliveryGoodsPlacement(GridPagingInfo pagingInfo,int dealerId,long deliveryId)
        {
            var response = new ListResponse<DeliveryListItem>();
            response.Items = _dbHelper.ExecuteListReader<DeliveryListItem>("PT_LIST_DELIVERY_GOODS_PLACEMENT",
                dealerId,
                MakeDbNull(deliveryId),
                pagingInfo.CurrentPage,
                pagingInfo.PageSize,
                0
                );
            pagingInfo.Total = int.Parse(_dbHelper.GetOutputValue("TOTAL").ToString());
            response.PageInfo = pagingInfo;
            return response;
        }
        public DeliveryInfo GetDeliveryInfo(long deliveryId,int dealerId)
        {
            return _dbHelper.ExecuteReader<DeliveryInfo>("PT_GET_DELIVERY_INFO",
                dealerId,
                deliveryId
                );
        }
        public ListResponse<DeliveryDetailListItem> ListDeliveryDetails(GridPagingInfo pagingInfo, int dealerId, long deliveryId)
        {
            var response = new ListResponse<DeliveryDetailListItem>();
            response.Items = _dbHelper.ExecuteListReader<DeliveryDetailListItem>("PT_LIST_DELIVERY_DETAILS",
                dealerId,
                MakeDbNull(deliveryId),
                pagingInfo.CurrentPage,
                pagingInfo.PageSize,
                0
                );
            pagingInfo.Total = int.Parse(_dbHelper.GetOutputValue("TOTAL").ToString());
            response.PageInfo = pagingInfo;
            return response;
        }

        public DeliveryDetailInfo GetDeliveryDetailInfo(long seqNo, int dealerId)
        {
            return _dbHelper.ExecuteReader<DeliveryDetailInfo>("PT_GET_DELIVERY_DETAIL_INFO",
                dealerId,
                seqNo
                );
        }
    }
}
