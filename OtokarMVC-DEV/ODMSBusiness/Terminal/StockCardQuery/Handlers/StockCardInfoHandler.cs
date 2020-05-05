using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockCardQuery.Dtos;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.StockCardQuery.Handlers
{
    public class StockCardInfoHandler:IHandleRequest<StockCardInfoRequest,StockCardInfo>
    {
        private readonly DbHelper _dbHelper;

        public StockCardInfoHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public StockCardInfo Handle(StockCardInfoRequest request)
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

            var stockCardInfo = _dbHelper.ExecuteReader<StockCardInfo>("P_TERMINAL_GET_STOCK_CARD_PART_INFO", 
                request.DealerId,
                Utility.MakeDbNull(barCode),
                Utility.MakeDbNull(partCode),
                request.LanguageCode,
                null
                );
            bool partExists = Convert.ToBoolean(_dbHelper.GetOutputValue("PART_EXISTS"));
            if (!partExists)
                throw new ODMSException(MessageResource.Exception_PartNotFound);
            if(stockCardInfo.Quantity==0)
                    throw new ODMSException(MessageResource.Exception_NoStock);
            return stockCardInfo;

        }
    }
}
