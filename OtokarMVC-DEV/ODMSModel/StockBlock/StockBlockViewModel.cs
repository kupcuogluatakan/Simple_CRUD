using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockBlock
{
    [Validator(typeof(StockBlockViewModelValidator))]
    public class StockBlockViewModel : ModelBase
    {
        public StockBlockViewModel()
        {
        }

        public Int64? IdStockBlock { get; set; }

        [Display(Name = "StockBlock_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }
        [Display(Name = "StockBlock_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "StockBlock_Display_BlockDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? BlockDate { get; set; }

        [Display(Name = "StockBlock_Display_BlockReasonDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BlockReasonDesc { get; set; }

        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int BlockedStatusId { get; set; }

        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BlockedStatusName { get; set; }

        public decimal? BlockQty { get; set; }
    }
}
