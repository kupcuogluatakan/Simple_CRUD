using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockMovement.Dtos
{
    public class DealerWareHouseListRequest :
        IRequest<DealerWareHouseListRequest, EnumerableResponse<DealerWareHouseListItem>>
    {
        public bool IncludeRacks { get; }
        public int DealerId { get; }

        public DealerWareHouseListRequest(int dealerId, bool includeRacks)
        {
            IncludeRacks = includeRacks;
            DealerId = dealerId;
        }
    }
}
