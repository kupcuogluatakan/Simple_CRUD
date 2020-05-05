using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.OldRackQuery.Dtos;
using ODMSBusiness.Terminal.StockCardQuery.Handlers;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.OldRackQuery.Handlers
{
    public class OldRackQueryInfoHandler:IHandleRequest<OldRackQueryInfoRequest,OldRackQueryInfo>
    {
        private readonly DbHelper _dbHelper;

        public OldRackQueryInfoHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public OldRackQueryInfo Handle(OldRackQueryInfoRequest request )
        {
            string partCode;
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

            var oldRackQueryInfo = _dbHelper.ExecuteReader<OldRackQueryInfo>("P_TERMINAL_GET_PART_INFO",
                request.DealerId,
                Utility.MakeDbNull(barCode),
                Utility.MakeDbNull(partCode),
                request.LanguageCode,
                null
                );
            bool partExists = Convert.ToBoolean(_dbHelper.GetOutputValue("PART_EXISTS"));
            if (!partExists)
                throw new ODMSException(MessageResource.Exception_PartNotFound);
            return oldRackQueryInfo;
        }

    }
}
