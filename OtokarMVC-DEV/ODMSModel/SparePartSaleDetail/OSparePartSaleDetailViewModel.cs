using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.SparePartSaleDetail
{
    [Validator(typeof(OSparePartSaleDetailViewModelValidator))]
    public class OSparePartSaleDetailViewModel : ModelBase
    {
        public Int64 SparePartSaleDetailId { get; set; }

        public Int64 SparePartSaleId { get; set; }
        
        [Display(Name = "OtokarPartSaleDetail_Display_PartNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartNameCode { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_PartNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PartId { get; set; }

        [Display(Name = "OtokarSparePartSaleDetail_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? Quantity { get; set; }

        [Display(Name = "OtokarSparePartSaleDetail_Display_PlanQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? PlanQuantity { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_DealerPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Price { get; set; }
        [Display(Name = "OtokarPartSaleDetail_Display_DiscountPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? DiscountPrice { get; set; }
        
        [Display(Name = "OtokarPartSaleDetail_Display_ReasonText", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ReturnReasonText { get; set; }

        [Display(Name = "OtokarPartSaleDetail_Display_VatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? VatRatio { get; set; }

        public int StockTypeId { get; set; }

        public int? MaxQuantity { get; set; }
    }
}
