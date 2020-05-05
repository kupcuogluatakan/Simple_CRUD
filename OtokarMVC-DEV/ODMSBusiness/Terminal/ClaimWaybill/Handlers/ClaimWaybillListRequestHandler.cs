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
    public class ClaimWaybillListRequestHandler:IHandleRequest<ClaimWaybillListRequest, EnumerableResponse<ClaimWaybillListItem>>
    {
        private readonly DbHelper _dbHelper;

        public ClaimWaybillListRequestHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<ClaimWaybillListItem> Handle(ClaimWaybillListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<ClaimWaybillListItem>("P_TERMINAL_LIST_CLAIM_WAYBILLS",
                request.DealerId);

            return  new EnumerableResponse<ClaimWaybillListItem>(list);
        }
    }
}
