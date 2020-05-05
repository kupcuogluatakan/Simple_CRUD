using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSModel.ViewModel;

namespace ODMSModel.CampaignPart
{
    [Validator(typeof (CampaignPartViewModelValidator))]
    public class CampaignPartViewModel : ModelBase
    {

        [Display(Name = "SparePartCountryVatRatio_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }

        public CampaignPartViewModel()
        {
            PartSearch = new AutocompleteSearchViewModel();
        }

        public bool HideFormElements { get; set; }

        [Display(Name = "Campaign_Display_CampaignCode",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }

        public Int64? PartId { get; set; }

        [Display(Name = "CampaignPart_Display_PartCode", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "CampaignPart_Display_PartTypeDesc", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string PartTypeDesc { get; set; }

        [Display(Name = "CampaignPart_Display_Quantity", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public decimal? Quantity { get; set; }

        public int? SupplyType { get; set; }

        [Display(Name = "CampaignPart_Display_SupplyType", ResourceType = typeof (ODMSCommon.Resources.MessageResource))
        ]
        public string SupplyTypeName { get; set; }
    }
}
