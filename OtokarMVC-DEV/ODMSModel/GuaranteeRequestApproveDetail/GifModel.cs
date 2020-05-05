using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class GifModel
    {
        [Display(Name = "GRADGif_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 WorkOrderId { get; set; }

        [Display(Name = "GRADGif_Display_WorkOrderDetId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 WorkOrderDetId { get; set; }

        [Display(Name = "WorkOrderCard_Dislay_VehicleLeaveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleLeaveDate { get; set; }

        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorName { get; set; }

        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessName { get; set; }

        [Display(Name = "GRADGif_Display_BrokenDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BrokenDate { get; set; }

        [Display(Name = "Campaign_Display_CampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignName { get; set; }

        [Display(Name = "SparePart_Display_VehicleKm", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleKm { get; set; }

        [Display(Name = "GRADGif_Display_GifNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GifNo { get; set; }

        [Display(Name = "GRADGif_Display_SpecialNotes", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SpecialNotes { get; set; }

        [Display(Name = "GRADGif_Display_Notes", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Notes { get; set; }

        public bool IsPerm { get; set; }
        [Display(Name = "GRADGif_Display_TxNote", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TxNote { get; set; }
    }
}
