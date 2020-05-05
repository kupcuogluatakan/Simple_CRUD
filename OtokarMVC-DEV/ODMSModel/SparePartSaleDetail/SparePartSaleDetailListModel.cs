using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.SparePartSaleDetail
{
    public class SparePartSaleDetailListModel : BaseListWithPagingModel
    {
        public string SoNumber { get; set; }
        public long SparePartId { get; set; }
        [Display(Name = "Global_Display_Select", ResourceType = typeof(MessageResource))]
        public long PartSaleId { get; set; }
        public int SparePartSaleDetailId { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_IsPriceFixed", ResourceType = typeof(MessageResource))]
        public bool IsPriceFixed { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string SparePartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string SparePartName { get; set; }

        [Display(Name = "Global_Display_DiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal? DiscountRatio { get; set; }

        [Display(Name = "Global_Display_PlanQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PlanQuantity { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_PickQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PickQuantity { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_PoQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PoQuantity { get; set; }

        [Display(Name = "SparePart_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal VatRatio { get; set; }

        [Display(Name = "SparePartDetails_Display_StockQuantity", ResourceType = typeof(MessageResource))]
        public decimal StockQuantity { get; set; }
        public string ReturnReasonText { get; set; }
        public int StatusId { get; set; }
        [Display(Name = "SparePartDetails_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_ListPrice", ResourceType = typeof(MessageResource))]
        public decimal? ListPrice { get; set; }
        public long? ChangedPartId { get; set; }
        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_ListPrice", ResourceType = typeof(MessageResource))]
        public string ListPriceWithCurrency { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_DealerPrice", ResourceType = typeof(MessageResource))]
        public decimal? DealerPrice { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_DealerPrice", ResourceType = typeof(MessageResource))]
        public string DealerPriceWithCurrency { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_ProfitMarginRatio", ResourceType = typeof(MessageResource))]
        public decimal? ProfitMarginRatio { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_DiscountPrice", ResourceType = typeof(MessageResource))]
        public decimal? DiscountPrice { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_DiscountPrice", ResourceType = typeof(MessageResource))]
        public string DiscountPriceWithCurrency { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_CalculatedPrice", ResourceType = typeof(MessageResource))]
        public decimal? CalculatedPrice { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_CalculatedPrice", ResourceType = typeof(MessageResource))]
        public string CalculatedPriceWithCurrency { get; set; }

        public int? IsTenderSale { get; set; }

        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(MessageResource))]
        public string Unit { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_PoOrderNo", ResourceType = typeof(MessageResource))]
        public long PoOrderNo { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_PoOrderLine", ResourceType = typeof(MessageResource))]
        public long PoOrderLine { get; set; }

        public bool IsCustomerDealer { get; set; }

        public SparePartSaleDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"SparePartId", "ID_PART"},
                     {"SparePartSaleId", "ID_PART_SALE"},
                     {"SparePartName", "PART_NAME"},
                     {"DiscountRatio", "DISCOUNT_RATIO"},
                     {"Quantity", "QUANTITY"},
                     {"VatRatio","VAT_RATIO"},
                     {"StockQuantity","STOCK_QUANTITY"},
                     {"PlanQuantity","PLAN_QUANTITY"},
                     {"StatusName","STATUS_NAME"},
                     {"ListPrice","LIST_PRICE"},
                     {"DealerPrice","DEALER_PRICE"},
                     {"DiscountPrice","DISCOUNT_PRICE"},
                     {"Unit","SP.UNIT_LOOKVAL"}
                 };
            SetMapper(dMapper);
        }

        public SparePartSaleDetailListModel() { }
        public string SoDetSeqNo { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_PickedQuantity", ResourceType = typeof(MessageResource))]
        public decimal? PickedQuantity { get; set; }
        [Display(Name = "SparePartSaleDetail_Display_ReturnedQuantity", ResourceType = typeof(MessageResource))]
        public decimal? ReturnedQuantity { get; set; }
    }
}
