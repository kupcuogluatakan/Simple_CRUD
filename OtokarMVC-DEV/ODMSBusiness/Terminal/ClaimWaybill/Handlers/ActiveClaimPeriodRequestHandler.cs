using ODMSBusiness.Terminal.ClaimWaybill.Dtos;
using ODMSBusiness.Terminal.Common;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.ClaimWaybill.Handlers
{
    public class ActiveClaimPeriodRequestHandler:IHandleRequest<ActiveClaimPeriodRequest, ActiveClaimPeriodInfo>
    {
        private readonly DbHelper _dbHelper;

        public ActiveClaimPeriodRequestHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public ActiveClaimPeriodInfo Handle(ActiveClaimPeriodRequest request)
        {
            return _dbHelper.ExecuteReader<ActiveClaimPeriodInfo>("P_TERMINAL_GET_ACTIVE_CLAIM_PERIOD_INFO");
        }

    }
}
