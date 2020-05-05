using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.OldRackQuery.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.OldRackQuery.Handlers
{
    public class OldRackQueryListHandler:IHandleRequest<OldRackQueryListRequest,EnumerableResponse<OldRackQueryListItem>>
    {
        private readonly DbHelper _dbHelper;

        public OldRackQueryListHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<OldRackQueryListItem> Handle(OldRackQueryListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<OldRackQueryListItem>("P_TERMINAL_LIST_OLD_RACK_ITEMS", request.PartId,
                request.DealerId);
            return new EnumerableResponse<OldRackQueryListItem>(list);
        }

    }
}
