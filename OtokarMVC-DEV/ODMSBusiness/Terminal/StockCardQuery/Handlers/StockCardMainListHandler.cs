using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockCardQuery.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.StockCardQuery.Handlers
{
    public class StockCardMainListHandler:IHandleRequest<StockCardMainListRequest,EnumerableResponse<StockCardMainListItem>>
    {
        private readonly DbHelper _dbHelper;

        public StockCardMainListHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<StockCardMainListItem> Handle(StockCardMainListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<StockCardMainListItem>("P_TERMINAL_LIST_STOCK_CARD_MAIN",
                request.DealerId,
                request.PartId,
                request.LanguageCode
                );
            return new EnumerableResponse<StockCardMainListItem>(list);
        }

    }
}
