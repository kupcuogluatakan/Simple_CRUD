using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.DeliveryGoodsPlacement;
using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;

namespace ODMSTerminal.Models
{
    public class DeliveryDetailModel
    {
        public DeliveryInfo DeliveryInfo { get;  }
        public ListResponse<DeliveryDetailListItem> Details { get;  }
        public DeliveryDetailModel(DeliveryInfo deliveryInfo, ListResponse<DeliveryDetailListItem> details)
        {
            DeliveryInfo = deliveryInfo;
            Details = details;
        }
    }
}