using ODMSBusiness.Terminal.ClaimWaybill.Dtos;
using ODMSBusiness.Terminal.Common;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.ClaimWaybill.Handlers
{
    public class ClaimWaybillPartListRequestHandler:IHandleRequest<ClaimWaybillPartListRequest,EnumerableResponse<ClaimWaybillPartListItem>>
    {
        private readonly DbHelper _dbHelper;

        public ClaimWaybillPartListRequestHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<ClaimWaybillPartListItem> Handle(ClaimWaybillPartListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<ClaimWaybillPartListItem>("P_TERMINAL_LIST_CLAIM_DISTMANTLED_PARTS", 
               request.ClaimWaybillId,
               request.DealerId,
               request.LanguageCode
                );
            return new EnumerableResponse<ClaimWaybillPartListItem>(list);
        }
    }
}
