using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.RackQuery.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.RackQuery.Handlers
{
    public class RackQueryListHandler:IHandleRequest<RackQueryListRequest,EnumerableResponse<RackQueryListItem>>
    {
        private readonly DbHelper _dbHelper;

        public RackQueryListHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<RackQueryListItem> Handle(RackQueryListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<RackQueryListItem>("P_TERMINAL_LIST_RACK_QUERY", 
                request.DealerId,
                request.WarehouseId,
                request.RackId,
                request.LanguageCode
                );
            return new EnumerableResponse<RackQueryListItem>(list);
        }
    }
}
