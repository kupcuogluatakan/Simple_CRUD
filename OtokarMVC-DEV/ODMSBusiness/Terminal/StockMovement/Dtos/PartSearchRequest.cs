using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockMovement.Dtos
{
    public class PartSearchRequest:IRequest<PartSearchRequest,PartSearchInfo>
    {
        public string LanguageCode { get; }
        public int DealerId { get; }
        public int WarehouseId { get; }
        public int RackId { get; }
        public string PartCode { get; }

        public PartSearchRequest(int dealerId,int warehouseId,int rackId, string 
            partCode,string languageCode)
        {
            LanguageCode = languageCode;
            DealerId = dealerId;
            WarehouseId = warehouseId;
            RackId = rackId;
            PartCode = partCode;
        }
    }
}
