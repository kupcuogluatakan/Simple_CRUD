using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
namespace ODMSModel.LabourCountryVatRatio
{
    [Validator(typeof(LabourCountryVatRatioViewModelValidator))]
    public class LabourCountryVatRatioViewModel:ModelBase
    {
        [Display(Name = "LabourDuration_Display_Labour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? LabourId { get; set; }
        [Display(Name = "LabourDuration_Display_Labour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourName { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountryName { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? CountryId { get; set; }
        [Display(Name = "SparePart_Display_VatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? VatRatio { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveString { get; set; }

        public int subCategoryId { get; set; }
        public int? mainCategoryId { get; set; }

    }
}
