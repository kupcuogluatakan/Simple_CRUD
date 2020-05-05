using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class PickingDetailInfo:IResponse
    {
        public long PickingId { get; set; }
        public long PickingDetailId { get; set; }
        public long PartId { get; set; }
        public string PartCode { get; set; }
        public decimal LeftQuantity { get; set; }

        public EnumerableResponse<StockRackListItem> StockRackList { get; set; }

    }
}
