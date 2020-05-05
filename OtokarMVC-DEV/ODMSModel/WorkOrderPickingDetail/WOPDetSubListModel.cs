using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.WorkOrderPickingDetail
{
    public class WOPDetSubListModel :ModelBase
    {
        public WOPDetSubListModel()
        {

        }

        public Int64 WOPDetId { get; set; }

        public int ResultId { get; set; }

        [UIHint("RackListbyPartEditor")] 
        public string Text { get; set; }

        [UIHint("RackListbyPartEditor")]
        [Display(Name = "WorkOrderPickingDet_Display_RackWarehouse", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Value { get; set; }

        [Display(Name = "WorkOrderPickingDet_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }
    }
}
