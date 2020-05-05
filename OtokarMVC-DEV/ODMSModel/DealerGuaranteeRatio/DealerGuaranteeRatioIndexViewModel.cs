using ODMSCommon.Resources;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.DealerGuaranteeRatio
{
    [Validator(typeof(DealerGuaranteeRatioIndexViewModelValidator))]
    public class DealerGuaranteeRatioIndexViewModel : ModelBase
    {
        public DealerGuaranteeRatioIndexViewModel()
        {

        }

        [Display(Name = "DealerGuaranteeRatio_Display_IdDealer", ResourceType = typeof(MessageResource))]
        public int? IdDealer { get; set; }

        [Display(Name = "DealerGuaranteeRatio_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "DealerGuaranteeRatio_Display_GuaranteeRatio", ResourceType = typeof(MessageResource))]
        public decimal? GuaranteeRatio { get; set; }

        [Display(Name = "DealerGuaranteeRatio_Display_DealerSSID", ResourceType = typeof(MessageResource))]
        public string DealerSSID { get; set; }

    }
}
