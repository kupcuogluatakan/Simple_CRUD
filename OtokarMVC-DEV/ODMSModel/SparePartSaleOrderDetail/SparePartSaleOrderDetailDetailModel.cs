using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartSaleOrderDetail
{
    [Validator(typeof(SparePartSaleOrderDetailDetailModelValidator))]
    public class SparePartSaleOrderDetailDetailModel : ModelBase
    {
        [Display(Name = "Global_Display_Select", ResourceType = typeof(MessageResource))]
        public string SoNumber { get; set; }
        public long SparePartSaleOrderDetailId { get; set; }

        public long SparePartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string SparePartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string SparePartName { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_IsOriginalPart", ResourceType = typeof(MessageResource))]
        public bool? IsOriginalPart { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_OrderQuantity", ResourceType = typeof(MessageResource))]
        public decimal? OrderQuantity { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_PlannedQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PlannedQuantity { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_ShippedQuantity", ResourceType = typeof(MessageResource))]
        public decimal? ShippedQuantity { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_ListPrice", ResourceType = typeof(MessageResource))]
        public decimal? ListPrice { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_OrderPrice", ResourceType = typeof(MessageResource))]
        public decimal? OrderPrice { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_ConfirmPrice", ResourceType = typeof(MessageResource))]
        public decimal? ConfirmPrice { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_ListDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? ListDiscountRatio { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_AppliedDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? AppliedDiscountRatio { get; set; }

        public string PoDetailSequenceNo { get; set; }
        public decimal? POQuantity { get; set; }
        public long? POOrderNo { get; set; }
        public long? POOrderLine { get; set; }
        public int? MasterStatusId { get; set; }

        public bool? MasterIsPriceFixed { get; set; }
        public int? StatusId { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_Status", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "SparePartSaleOrderDetail_Display_SpecialExplanation", ResourceType = typeof(MessageResource))]
        public string SpecialExplanation { get; set; }
    }
}
