using System.Collections.Generic;
using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;

namespace ODMSTerminal.Models
{
    public class DeliveryPlacementModel
    {
        public DeliveryDetailInfo DetailInfo { get;  }
        public List<PartsPlacementListItem> PartsPlacementList { get; }

        public DeliveryPlacementModel(DeliveryDetailInfo deliveryDetailInfo, List<PartsPlacementListItem> partsPlacementList)
        {
            DetailInfo = deliveryDetailInfo;
            PartsPlacementList = partsPlacementList;
        }
    }
}