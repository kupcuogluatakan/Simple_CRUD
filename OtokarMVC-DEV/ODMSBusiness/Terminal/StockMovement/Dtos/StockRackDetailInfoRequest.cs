using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockMovement.Dtos
{
   public class StockRackDetailInfoRequest:IRequest<StockRackDetailInfoRequest,StockRackDetailInfo>
   {
       public int DealerId { get; }
       public int WarehouseId { get; }
       public int RackId { get; }
       public long PartId { get; }
       public string LanguageCode { get; }

       public StockRackDetailInfoRequest(int dealerId,int warehouseId,int rackId, long partId,string languageCode)
       {
           DealerId = dealerId;
           WarehouseId = warehouseId;
           RackId = rackId;
           PartId = partId;
           LanguageCode = languageCode;
       }
   }
}
