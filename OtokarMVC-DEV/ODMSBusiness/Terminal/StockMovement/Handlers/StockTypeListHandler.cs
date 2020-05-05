using System.Web.Mvc;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockMovement.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.StockMovement.Handlers
{
    public class StockTypeListHandler:IHandleRequest<StockTypeListRequest,EnumerableResponse<SelectListItem>>
    {
        private readonly DbHelper _dbHelper;

        public StockTypeListHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<SelectListItem> Handle(StockTypeListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<SelectListItem>("P_TERMINAL_LIST_STOCK_TYPES",
                request.DelarId,
                request.WarehouseId,
                request.PartId,
                request.LanguageCode
                );
            var response = new EnumerableResponse<SelectListItem>(list);
            return response;
        }
    }
}
