using System.Collections.Generic;
using ODMSModel.WorkOrderCard.WarrantyDetail;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderCardWarrantyModel
    {
        public long WorkOrderId { get; set; }
        public long WorkOrderDetailId { get; set; }
        public bool IsWarrantyClaimed { get; set; }
        public List<WarrantyDetailItem> Items { get; set; }

        public WorkOrderCardWarrantyModel()
        {
            Items= new List<WarrantyDetailItem>();
        }
    }
}
