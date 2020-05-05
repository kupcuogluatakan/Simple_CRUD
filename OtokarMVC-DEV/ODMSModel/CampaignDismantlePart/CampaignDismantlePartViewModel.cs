using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;
using ODMSModel.ViewModel;

namespace ODMSModel.CampaignDismantlePart
{
    [Validator(typeof(CampaignDismantlePartModelValidator))]
    public class CampaignDismantlePartViewModel : ModelBase
    {
        public CampaignDismantlePartViewModel()
        {
            PartSearch = new AutocompleteSearchViewModel();
        }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }

        [Display(Name = "Campaign_Display_CampaignCode", ResourceType = typeof(MessageResource))]
        public string CampaignCode { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public int? PartId { get; set; }

        [Display(Name = "CampaignPart_Display_PartTypeDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartTypeDesc { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "Campaign_Display_Note", ResourceType = typeof(MessageResource))]
        public string Note { get; set; }
    }
}
