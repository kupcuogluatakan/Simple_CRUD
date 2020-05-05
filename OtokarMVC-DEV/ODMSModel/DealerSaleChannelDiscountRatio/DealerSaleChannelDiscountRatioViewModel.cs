using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.DealerSaleChannelDiscountRatio
{
    [Validator(typeof(DealerSaleChannelDiscountRatioViewModelValidator))]
    public class DealerSaleChannelDiscountRatioViewModel : ModelBase
    {
        [Display(Name = "DealerSaleChannelDiscountRatios_Display_DealerClassCode", ResourceType = typeof(MessageResource))]
        public string DealerClassCode { get; set; }
        [Display(Name = "DealerSaleChannelDiscountRatios_Display_DealerClassName", ResourceType = typeof(MessageResource))]
        public string DealerClassName { get; set; }

        [Display(Name = "DealerSaleChannelDiscountRatios_Display_ChannelCode", ResourceType = typeof(MessageResource))]
        public string ChannelCode { get; set; }
        [Display(Name = "DealerSaleChannelDiscountRatios_Display_ChannelName", ResourceType = typeof(MessageResource))]
        public string ChannelName { get; set; }

        [Display(Name = "DealerSaleChannelDiscountRatios_Display_SparePartClassCode", ResourceType = typeof(MessageResource))]
        public string SparePartClassCode { get; set; }

        [Display(Name = "DealerSaleChannelDiscountRatios_Display_TseValidDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? TseValidDiscountRatio { get; set; }

        [Display(Name = "DealerSaleChannelDiscountRatios_Display_TseInvalidDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? TseInvalidDiscountRatio { get; set; }
    }
}
