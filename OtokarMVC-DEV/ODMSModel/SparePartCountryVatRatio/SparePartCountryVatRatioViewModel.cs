using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.SparePartCountryVatRatio
{
    [Validator(typeof(SparePartCountryVatRatioViewModelValidator))]
    public class SparePartCountryVatRatioViewModel : ModelBase
    {
        public SparePartCountryVatRatioViewModel()
        {
            PartSearch = new AutocompleteSearchViewModel();
        }

        public bool HideFormElements { get; set; }
        public string MultiLanguageContentAsText { get; set; }

        [Display(Name = "SparePartCountryVatRatio_Display_PartName", ResourceType = typeof(MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }

        //IdPart
        [Display(Name = "SparePartCountryVatRatio_Display_PartCode", ResourceType = typeof(MessageResource))]
        public Int64? IdPart { get; set; }

        //PartCode
        [Display(Name = "SparePartCountryVatRatio_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        //PartName
        [Display(Name = "SparePartCountryVatRatio_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        //IdCountry
        [Display(Name = "SparePartCountryVatRatio_Display_CountryName", ResourceType = typeof(MessageResource))]
        public int? IdCountry { get; set; }
        //CountryName
        [Display(Name = "SparePartCountryVatRatio_Display_CountryName", ResourceType = typeof(MessageResource))]
        public string CountryName { get; set; }

        //VatRatio
        [Display(Name = "SparePartCountryVatRatio_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal? VatRatio { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
