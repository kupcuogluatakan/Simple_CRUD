using System.Collections.Generic;

namespace ODMSModel.WorkOrderPickingDetail
{
    public class WOPDetSubViewModel : ModelBase
    {
        public int ResultId { get; set; }
        public List<WOPDetSubListModel> ListSubModel { get; set; }
    }
}
