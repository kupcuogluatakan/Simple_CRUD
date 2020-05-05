using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.ClaimWaybill.Dtos;
using ODMSBusiness.Terminal.Common;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.ClaimWaybill.Handlers
{
    public class ClaimWaybillSearchPartRequestHandler: IHandleRequest<ClaimWaybillSearchPartRequest, ClaimWaybillSearchPartResponse>
    {
        private readonly DbHelper _dbHelper;

        public ClaimWaybillSearchPartRequestHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public ClaimWaybillSearchPartResponse Handle(ClaimWaybillSearchPartRequest request)
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

            var response =
                _dbHelper.ExecuteReader<ClaimWaybillSearchPartResponse>("P_TERMINAL_SEARCH_CLAIM_DISMANTLED_PART",
                    request.ClaimWaybillId,
                    Utility.MakeDbNull(barCode),
                    Utility.MakeDbNull(partCode),
                    request.DealerId
                    );
            if (response == null || response.ClaimDismantledPartId==0)
                throw new ODMSException(MessageResource.Exception_PartNotFound);
            return response;
        }
    }
}
