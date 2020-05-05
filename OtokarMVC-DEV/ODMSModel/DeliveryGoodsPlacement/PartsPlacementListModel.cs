using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.DeliveryGoodsPlacement
{
    public class PartsPlacementListModel
    {
        public Int64 DeliveryId { get; set; }
        public Int64 DeliverySeqNo { get; set; }
        public Int64 PlacementId { get; set; }

        [UIHint("RackListbyPartEditorDelivery")]
        public string Text { get; set; }

        [UIHint("RackListbyPartEditorDelivery")]
        [Display(Name = "WorkOrderPickingDet_Display_RackWarehouse", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Value { get; set; }

        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }
    }
}
