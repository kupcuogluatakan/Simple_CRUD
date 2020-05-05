using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.DealerSaleChannelDiscountRatio
{
    public class DealerSaleChannelDiscountRatioListModel : BaseListWithPagingModel
    {
        public DealerSaleChannelDiscountRatioListModel()
        {

        }

        public DealerSaleChannelDiscountRatioListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"DealerClassCode", "DEALER_CLASS_CODE"},
                     {"DealerClassName", "DEALER_CLASS_NAME"},
                     {"ChannelCode", "CHANNEL_CODE"},
                     {"ChannelName", "CHANNEL_NAME"},
                     {"SparePartClassCode", "SPARE_PART_CLASS_CODE"},
                     {"TseValidDiscountRatio", "TSE_VALID_DISCOUNT_RATIO"},
                     {"TseInvalidDiscountRatio", "TSE_INVALID_DISCOUNT_RATIO"}
                 };
            SetMapper(dMapper);
        }
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
        public decimal TseValidDiscountRatio { get; set; }

        [Display(Name = "DealerSaleChannelDiscountRatios_Display_TseInvalidDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal TseInvalidDiscountRatio { get; set; }
    }
}
