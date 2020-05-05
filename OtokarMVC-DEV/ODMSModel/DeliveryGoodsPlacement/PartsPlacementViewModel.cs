using System;
using System.Collections.Generic;

namespace ODMSModel.DeliveryGoodsPlacement
{
    public class PartsPlacementViewModel : ModelBase
    {
        public Int64 PlacementId { get; set; }
        public List<PartsPlacementListModel> ListModel { get; set; }
    }
}
