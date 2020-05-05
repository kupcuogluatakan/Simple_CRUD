using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.SparePart
{
    public class SparePartSupplyDiscountRatioListModel:BaseListWithPagingModel
    {
        public SparePartSupplyDiscountRatioListModel()
        {
                
        }

        public SparePartSupplyDiscountRatioListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"DiscountRatio","DISCOUNT_RATIO"},
                    {"ChannelName","CHANNEL_CODE"}
                };
            SetMapper(dMapper);
        }
        [Display(Name = "Dealer_Display_SaleChannel",ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ChannelName { get; set; }
        [Display(Name = "SparePart_Display_SupplyDiscountRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal DiscountRatio { get; set; }
        public long PartId { get; set; }
    }
}
