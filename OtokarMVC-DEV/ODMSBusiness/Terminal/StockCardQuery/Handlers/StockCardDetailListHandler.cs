using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockCardQuery.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.StockCardQuery.Handlers
{
    public class StockCardDetailListHandler:IHandleRequest<StockCardDetailListRequest,EnumerableResponse<StockCardDetailListItem>>
    {
        private readonly DbHelper _dbHelper;

        public StockCardDetailListHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<StockCardDetailListItem> Handle(StockCardDetailListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<StockCardDetailListItem>("P_TERMINAL_LIST_STOCK_CARD_DETAIL",
                request.DealerId,
                request.PartId,
                request.LanguageCode
                );
            return new EnumerableResponse<StockCardDetailListItem>(list);
        }
    }
}
