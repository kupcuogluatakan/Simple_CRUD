using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class DeliveryListRequest:IRequest<DeliveryListRequest, ListResponse<DeliveryListItem>>
    {
        public string SapDeliveryNo { get; }
        public string WaybillNo { get; }
        public GridPagingInfo PagingInfo { get; }
        public int DealerId { get; }
        public long DeliveryId { get; }

        public DeliveryListRequest(string sapDeliveryNo,string waybillNo,GridPagingInfo pagingInfo,int dealerId,long deliveryId)
        {
            SapDeliveryNo = sapDeliveryNo;
            WaybillNo = waybillNo;
            PagingInfo = pagingInfo;
            DealerId = dealerId;
            DeliveryId = deliveryId;
        }
       
    }
}
