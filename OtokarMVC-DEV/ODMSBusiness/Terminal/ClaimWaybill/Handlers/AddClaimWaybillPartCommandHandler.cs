using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.ClaimWaybill.Dtos;
using ODMSBusiness.Terminal.Common;
using ODMSCommon.Exception;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.ClaimWaybill.Handlers
{
    public class AddClaimWaybillPartCommandHandler:IHandleCommand<AddClaimWaybillPartCommand>
    {
        private readonly DbHelper _dbHelper;

        public AddClaimWaybillPartCommandHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void Handle(AddClaimWaybillPartCommand command)
        {
            _dbHelper.ExecuteNonQuery("P_SET_CLAIM_WAY_BILL",
                command.ClaimDismantledPartId,
                Utility.MakeDbNull(command.ClaimWaybillId),
                command.UserId,
                null,
                null
                );
            var errorNo = Convert.ToInt32(_dbHelper.GetOutputValue("ERROR_NO"));
            if (errorNo> 0)
                throw new ODMSException(Utility.ResolveDatabaseErrorXml(_dbHelper.GetOutputValue("ERROR_DESC").ToString()));
        }
    }
}
