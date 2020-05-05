using System;
using System.ComponentModel.DataAnnotations;
namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class GuaranteeLaboursListModel : ModelBase
    {
        public Int64 Id { get; set; }

        public Int64 GuaranteeId { get; set; }

        public int GuaranteeSeq { get; set; }

        [Display(Name = "CampaignLabour_Display_LabourCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Value { get; set; }

        [Display(Name = "CampaignLabour_Display_LabourCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Text { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Desc { get; set; }

        [Display(Name = "LabourDuration_Display_Duration", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Duration { get; set; }

        public bool IsDurationCheck { get; set; }

        [Display(Name = "GuaranteeParts_Display_Ratio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Ratio { get; set; }

        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Quantity { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_WarrantyUnitPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WarrantyPrice { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_WarrantyTotal", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WarrantyTotal { get; set; }

    }
}
