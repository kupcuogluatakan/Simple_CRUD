using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.StockCard
{
    [Validator(typeof (StockCardViewModelValidator))]
    public class StockCardViewModel : ModelBase
    {
        public int StockCardId { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "StockCard_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        public int? PartDealerId { get; set; }
        [Display(Name = "StockCard_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartDealerName { get; set; }

        public int? PartId { get; set; }
        [Display(Name = "StockCard_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "StockCard_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public bool IsOriginalPart { get; set; }
        [Display(Name = "StockCard_Display_IsOriginalPartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsOriginalPartName { get; set; }
        
        public int? WarehouseId { get; set; }
        [Display(Name = "StockCard_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        public int? RackId { get; set; }
        [Display(Name = "StockCard_Display_RackName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackName { get; set; }

        public bool AutoOrder { get; set; }
        [Display(Name = "StockCard_Display_AutoOrderName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AutoOrderName { get; set; }

        [Display(Name = "StockCard_Display_CriticalStockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? CriticalStockQuantity { get; set; }

        [Display(Name = "StockCard_Display_MinStockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? MinStockQuantity { get; set; }

        [Display(Name = "StockCard_Display_MaxStockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? MaxStockQuantity { get; set; }

        [Display(Name = "StockCard_Display_AvgDealerPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal AvgDealerPrice { get; set; }

        [Display(Name = "StockCard_Display_MinSaleQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? MinSaleQuantity { get; set; }

        [Display(Name = "StockCard_Display_SalePrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? SalePrice { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_AltenativeCostPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? CostPrice { get; set; }

        [Display(Name = "StockCard_Display_ProfitMarginRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? ProfitMarginRatio { get; set; }

        [Display(Name = "StockCard_Display_Weight", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Weight { get; set; }

        [Display(Name = "StockCard_Display_Volume", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Volume { get; set; }

        [Display(Name = "StockCard_Display_VatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? VatRatio { get; set; }

        [Display(Name = "StockCard_Display_SaleChannelDiscountRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? SaleChannelDiscountRatio { get; set; }

        [Display(Name = "SparePart_Display_LeadTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? LeadTime { get; set; }

        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(MessageResource))]
        public string UnitName { get; set; }


        public bool CalculatedPrice
        {
            get { return SalePrice == null || SalePrice > AvgDealerPrice + (AvgDealerPrice*ProfitMarginRatio/100); }
        }
        [Display(Name = "StockCard_Display_StartupStockQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? StartupStockQty { get; set; }
        [Display(Name = "StockCard_Display_StockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? StockQuantity { get; set; }        
        [Display(Name = "StockCard_Display_Last_Po_Price", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? LastPrice { get; set; }


        [Display(Name = "StockCard_Display_Otokar_Stock", ResourceType = typeof(MessageResource))]
        public string StockServiceValue { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_AlternatePart", ResourceType = typeof(MessageResource))]
        public string AlternatePart { get; set; }
    }
}
