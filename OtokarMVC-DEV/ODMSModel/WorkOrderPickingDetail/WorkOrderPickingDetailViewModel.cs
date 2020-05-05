using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.WorkOrderPickingDetail
{
    public class WorkOrderPickingDetailViewModel : ModelBase
    {
        public Int64 WorkOrderPickingDetailId { get; set; }

        public long WorkOrderPickingMstId { get; set; }

        public long PartId { get; set; }

        public int StockTypeId { get; set; }

        public decimal RequiredQuantity { get; set; }
        public decimal PickQuantity { get; set; }
        public string PickClosureDescription { get; set; }
        public long WorkOrderDetailId { get; set; }
    }
}
