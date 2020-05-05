using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockMovement.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.StockMovement.Handlers
{
    public class DealerRackListHandler : IHandleRequest<DealerRackListRequest, EnumerableResponse<DealerRackListItem>>
    {
        private readonly DbHelper _dbHelper;

        public DealerRackListHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<DealerRackListItem> Handle(DealerRackListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<DealerRackListItem>("P_LIST_RACKS_COMBO", request.DealerId,
                request.WarehouseId);
            return new EnumerableResponse<DealerRackListItem>(list);
        }
    }
}