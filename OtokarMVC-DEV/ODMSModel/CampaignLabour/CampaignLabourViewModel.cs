using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.CampaignLabour
{
    [Validator(typeof(CampaignLabourViewModelValidator))]
    public class CampaignLabourViewModel : ModelBase
    {
        public CampaignLabourViewModel()
        {
        }

        public bool HideFormElements { get; set; }

        [Display(Name = "Campaign_Display_CampaignCode",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }

        public int? LabourId { get; set; }
        [Display(Name = "CampaignLabour_Display_LabourCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourCode { get; set; }

        [Display(Name = "CampaignLabour_Display_LabourTypeDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourTypeDesc { get; set; }

        [Display(Name = "CampaignLabour_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Quantity { get; set; }
    }
}
