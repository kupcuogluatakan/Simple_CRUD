using System.ComponentModel.DataAnnotations;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class GRADGifHistoryDetModel
    {
        [Display(Name = "Global_Display_Type", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Type { get; set; }

        [Display(Name = "Global_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Code { get; set; }

        [Display(Name = "Global_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Name { get; set; }

        [Display(Name = "WorkOrderCard_Display_WarrantyRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Ratio { get; set; }
    }
}
