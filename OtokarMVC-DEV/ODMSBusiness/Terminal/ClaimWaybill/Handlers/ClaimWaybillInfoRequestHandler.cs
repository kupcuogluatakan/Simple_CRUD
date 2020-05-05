using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.ClaimWaybill.Dtos;
using ODMSBusiness.Terminal.Common;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.ClaimWaybill.Handlers
{
    public class ClaimWaybillInfoRequestHandler : IHandleRequest<ClaimWaybillInfoRequest, ClaimWaybillInfo>
    {
        private readonly DbHelper _dbHelper;

        public ClaimWaybillInfoRequestHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public ClaimWaybillInfo Handle(ClaimWaybillInfoRequest request)
        {
            return _dbHelper.ExecuteReader<ClaimWaybillInfo>("P_TERMINAL_GET_CLAIM_WAYBILL_INFO",
                request.ClaimPeriodId,
                request.ClaimWaybillId,
                request.DealerId
                );
        }
    }
}
