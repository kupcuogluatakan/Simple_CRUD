using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSModel.ViewModel;

namespace ODMSModel.CriticalStockCard
{
    [Validator(typeof(CriticalStockCardViewModelValidator))]
    public class CriticalStockCardViewModel : ModelBase
    {
        public CriticalStockCardViewModel()
        {
            PartSearch = new AutocompleteSearchViewModel();
        }

        public int StockCardId { get; set; }

        public int? IdDealer { get; set; }
        [Display(Name = "StockCard_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        public Int64? IdPart { get; set; }

        [Display(Name = "CriticalStockCard_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "CriticalStockCard_Display_PartNameAndCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "CriticalStockCard_Display_CriticalStockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? CriticalStockQuantity { get; set; }

        public string SSID { get; set; }//For Excel import

        [Display(Name = "CriticalStockCard_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }

        [Display(Name = "CriticalStockCard_Display_ShipQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ShipQty { get; set; }
    }
}
