using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ODMSModel.DealerSaleSparepart
{
    [Validator(typeof(DealerSaleSparepartIndexViewModelValidator))]
    public class DealerSaleSparepartIndexViewModel : ModelBase
    {
        public DealerSaleSparepartIndexViewModel()
        {
            PartSearch = new AutocompleteSearchViewModel();
        }

        public bool HideFormElements { get; set; }
        public string MultiLanguageContentAsText { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_IdDealer", ResourceType = typeof(MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }

        //IdDealer
        [Display(Name = "DealerSaleSparepart_Display_IdDealer", ResourceType = typeof(MessageResource))]
        public int? IdDealer { get; set; }

        //IdPart
        [Display(Name = "DealerSaleSparepart_Display_IdPart", ResourceType = typeof(MessageResource))]
        public long? IdPart { get; set; }

        //DiscountRatio
        [Display(Name = "DealerSaleSparepart_Display_DiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? DiscountRatio { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_DiscountRatio", ResourceType = typeof(MessageResource))]
        public string DiscountRatioString
        {
            get { return Convert.ToString(DiscountRatio, CultureInfo.InvariantCulture); }
            set
            {
                decimal val;
                var result = decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                if (result)
                    DiscountRatio = val;
            }
        }

        //DiscountPrice
        [Display(Name = "DealerSaleSparepart_Display_DiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal? DiscountPrice { get; set; }

        //ListPrice
        [Display(Name = "DealerSaleSparepart_Display_ListPrice", ResourceType = typeof(MessageResource))]
        public decimal? ListPrice { get; set; }

        //PartName
        [Display(Name = "DealerSaleSparepart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        //PartCode
        [Display(Name = "DealerSaleSparepart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        //CreateDate
        [Display(Name = "DealerSaleSparepart_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public DateTime? CreateDate { get; set; }

        //StockQuantity
        [Display(Name = "DealerSaleSparepart_Display_StockQuantity", ResourceType = typeof(MessageResource))]
        public decimal? StockQuantity { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        //[Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        //public bool? IsActiveSearch { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_SalePrice", ResourceType = typeof(MessageResource))]
        public decimal? SalePrice { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_ShipQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ShipQty { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Unit { get; set; }
    }
}
