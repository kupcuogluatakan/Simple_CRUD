using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockMovement.Dtos
{
    public class DealerRackListRequest : IRequest<DealerRackListRequest, EnumerableResponse<DealerRackListItem>>
    {
        public DealerRackListRequest(int dealerId, int warehouseId)
        {
            DealerId = dealerId;
            WarehouseId = warehouseId;
        }

        public int DealerId { get; }
        public int WarehouseId { get; }
    }
}