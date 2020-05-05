using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.StockRackDetail
{
    [Validator(typeof (StockExchangeViewModelValidator))]
    public class StockExchangeViewModel : ModelBase
    {
        public int? StockTransactionId { get; set; }

        [Display(Name = "StockExchange_Display_FromRack", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? FromRackId { get; set; }
        public int FromWarehouseId { get; set; }

        [Display(Name = "StockExchange_Display_Part", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? PartId { get; set; }
        
        [Display(Name = "StockExchange_Display_ToRack", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? ToRackId { get; set; }
        public int ToWarehouseId { get; set; }

        [Display(Name = "StockExchange_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StockTypeId { get; set; }

        [Display(Name = "StockExchange_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Quantity { get; set; }
        public decimal MaxQuantity { get; set; } 

        [Display(Name = "StockExchange_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }
    }
}
