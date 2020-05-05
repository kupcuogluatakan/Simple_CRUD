using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockMovement.Dtos
{
    public class StockTypeListRequest:IRequest<StockTypeListRequest,EnumerableResponse<SelectListItem>>
    {
        public int DelarId { get; }
        public int WarehouseId { get; }
        public long PartId { get; }
        public string LanguageCode { get; }

        public StockTypeListRequest(int delarId,int warehouseId,long partId,string languageCode)
        {
            DelarId = delarId;
            WarehouseId = warehouseId;
            PartId = partId;
            LanguageCode = languageCode;
        }
    }
}
