using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockMovement.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.StockMovement.Handlers
{
    public class StockTypeDetailInfoGetHandler : IHandleRequest<StockRackDetailInfoRequest, StockRackDetailInfo>
    {
        private readonly DbHelper _dbHelper;

        public StockTypeDetailInfoGetHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public StockRackDetailInfo Handle(StockRackDetailInfoRequest request)
        {
            return _dbHelper.ExecuteReader<StockRackDetailInfo>("P_TERMINAL_GET_STOCK_RACK_DETAIL",
                request.DealerId,
                request.WarehouseId,
                request.RackId,
                request.PartId,
                request.LanguageCode
                );
        }
    }
}