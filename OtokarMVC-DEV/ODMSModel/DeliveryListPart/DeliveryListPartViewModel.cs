using System;
using System.Collections.Generic;

namespace ODMSModel.DeliveryListPart
{
    public class DeliveryListPartViewModel : ModelBase
    {
        public Int64 DeliveryId { get; set; }
        public List<DeliveryListPartListModel> ListModel { get; set; }
    }
}
