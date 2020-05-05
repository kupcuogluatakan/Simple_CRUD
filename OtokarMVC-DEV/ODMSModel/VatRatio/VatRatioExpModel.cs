using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.VatRatio
{
    [Validator(typeof(VatRatioExpModelValidator))]
    public class VatRatioExpModel:ModelBase
    {
        [Display(Name = "Vehicle_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal VatRatio { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(MessageResource))]
        public int? CountryId { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(MessageResource))]
        public string Country { get; set; }
        [Display(Name = "VatRatio_Display_Explaination", ResourceType = typeof(MessageResource))]
        public string Explation { get; set; }
        [Display(Name = "Global_Display_Warning", ResourceType = typeof(MessageResource))]
        public string Warning { get { return MessageResource.VatRatioExp_WarningMessage; } }    
    }
}
