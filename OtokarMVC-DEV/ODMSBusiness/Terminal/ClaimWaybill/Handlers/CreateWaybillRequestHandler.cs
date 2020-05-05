using System;
using ODMSBusiness.Terminal.ClaimWaybill.Dtos;
using ODMSBusiness.Terminal.Common;
using ODMSCommon.Exception;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.ClaimWaybill.Handlers
{
    public class CreateWaybillRequestHandler:IHandleRequest<CreateWaybillRequest, ClaimWaybillResponse>
    {
        private readonly DbHelper _dbHelper;


        public CreateWaybillRequestHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public ClaimWaybillResponse Handle(CreateWaybillRequest request)
        {
            _dbHelper.ExecuteNonQuery("P_DML_CLAIM_WAYBILL",
                null,
                request.DealerId,
                request.WaybillSerialNo,
                request.WaybillNo,
                request.WaybillDate,
                null,
                null,
                null,
                "I",
                request.UserId,
                null,
                null
                );
            int errorNo = Convert.ToInt32(_dbHelper.GetOutputValue("ERROR_NO"));
            if (errorNo > 0)
                throw new ODMSException(
                    Utility.ResolveDatabaseErrorXml(_dbHelper.GetOutputValue("ERROR_DESC").ToString()));
            return new ClaimWaybillResponse(request.ClaimPeriodId, Convert.ToInt32(_dbHelper.GetOutputValue("ID_CLAIM_WAYBILL")));
        }
    }
}
