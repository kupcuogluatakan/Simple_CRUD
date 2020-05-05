using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.CampaignVehicle
{
    [Validator(typeof (CampaignVehicleViewModelValidator))]
    public class CampaignVehicleViewModel : ModelBase
    {
        public CampaignVehicleViewModel()
        {
        }

        public bool HideFormElements { get; set; }

        [Display(Name = "Campaign_Display_CampaignCode",
            ResourceType = typeof (MessageResource))]
        public string CampaignCode { get; set; }

        public int? VehicleId { get; set; }

        [Display(Name = "CampaignVehicle_Display_VinNo", ResourceType = typeof (MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        public bool IsUtilized { get; set; }

        [Display(Name = "CampaignVehicle_Display_IsUtilized",
            ResourceType = typeof (MessageResource))]
        public string IsUtilizedName { get; set; }
    }
}
