using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockMovement.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.StockMovement.Handlers
{
    public class PartSearchHandler:IHandleRequest<PartSearchRequest,PartSearchInfo>
    {
        private readonly DbHelper _dbHelper;


        public PartSearchHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public PartSearchInfo Handle(PartSearchRequest request)
        {
            string partCode = string.Empty;
            string barCode = string.Empty;
            if (request.PartCode.Contains(";"))
            {
                var arr = request.PartCode.Split(';');
                barCode = arr[0];
                partCode = arr[1];
            }
            else
            {
                partCode = request.PartCode;
            }
            var partSearchInfo = _dbHelper.ExecuteReader<PartSearchInfo>("P_TERMINAL_SEARCH_STOCK_RACK_DETAIL",
                request.DealerId,
                request.WarehouseId,
                request.RackId,
                Utility.MakeDbNull(partCode),
                Utility.MakeDbNull(barCode),
                request.LanguageCode
                );
            return partSearchInfo;
        }
    }
}
