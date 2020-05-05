using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System;

namespace ODMSModel.SparePartSaleDetail
{
    [Validator(typeof(SparePartSaleDetailDetailModelValidator))]
    public class SparePartSaleDetailDetailModel : ModelBase
    {


        public long? SparePartId { get; set; }
        public int PartSaleId { get; set; }
        public long SparePartSaleDetailId { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string SparePartCode { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string SparePartName { get; set; }
        [Display(Name = "Currency_Display_CurrencyName", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_ListPrice", ResourceType = typeof(MessageResource))]
        public decimal? ListPrice { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_DealerPrice", ResourceType = typeof(MessageResource))]
        public decimal? DealerPrice { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_DiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal? DiscountPrice { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_DiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? DiscountRatio { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal VatRatio { get; set; }
        public new int? StatusId { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_PlanQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PlanQuantity { get; set; }
        public string ReturnReasonText { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_AlternatePart", ResourceType = typeof(MessageResource))]
        public string AlternatePart { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_AltenativeCostPrice", ResourceType = typeof(MessageResource))]
        public decimal CostPrice { get; set; }
        public long DeliverySeqNo { get; set; }

        public bool IsPriceFixed { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_PickQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PickQuantity { get; set; }
        public decimal PickedQuantity { get; set; }
        public decimal? ReturnedQuantity { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_TotalListPrice", ResourceType = typeof(MessageResource))]
        public decimal TotalListPrice { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_TotalDiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal TotalDiscountPrice { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_TotalPriceWithoutVatRatio", ResourceType = typeof(MessageResource))]
        public decimal TotalPriceWithoutVatRatio { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_TotalPriceWithVatRatio", ResourceType = typeof(MessageResource))]
        public decimal TotalPriceWithVatRatio { get; set; }
        public bool? IsFirstPartOriginal { get; set; }
        public string DealerCurrencyCode { get; set; }
        public DateTime? PriceListDate { get; set; }
        public long? SparePartSaleWaybillId { get; set; }
        public long? SparePartSaleInvoiceId { get; set; }
        public string SoDetSeqNo { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_PoQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PoQuantity { get; set; }

        [Display(Name = "SparePartDetails_Display_StockQuantity", ResourceType = typeof(MessageResource))]
        public decimal StockQuantity { get; set; }



        [Display(Name = "SparePartSaleDetail_Display_PoOrderNo", ResourceType = typeof(MessageResource))]
        public long PoOrderNo { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_PoOrderLine", ResourceType = typeof(MessageResource))]
        public long PoOrderLine { get; set; }



        [Display(Name = "SparePartSaleDetail_Display_ShipmentQuantity", ResourceType = typeof(MessageResource))]
        public decimal? ShipmentQuantity { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_ProfitMarginRatio", ResourceType = typeof(MessageResource))]
        public decimal? ProfitMarginRatio { get; set; }
        public bool VatExcluded { get; set; }

    }
}
