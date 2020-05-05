using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.PickingList.Dtos;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.PickingList.Handlers
{
    public class StockRackListHandler:IHandleRequest<StockRackListRequest,EnumerableResponse<StockRackListItem>>
    {
        private readonly DbHelper _dbHelper;

        public StockRackListHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<StockRackListItem> Handle(StockRackListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<StockRackListItem>("P_TERMINAL_LIST_STOCK_RACK_FOR_PICKING",
                request.PickingDetailId,
                request.DealerId,
                Utility.MakeDbNull(request.RackCode)
                );
            if (!list.Any())
                throw new ODMSException(MessageResource.Exception_RackNotFound);
            return new EnumerableResponse<StockRackListItem>(list);
        }
    }
}
