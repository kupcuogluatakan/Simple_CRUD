using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockCard
{
    public class StockCardListModel : BaseListWithPagingModel
    {
        public StockCardListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartCode","PART_CODE"},
                    {"PartName","PART_NAME"},
                    {"DealerName", "DEALER_NAME"},
                    {"RackName","RACK_NAME"},
                    {"WarehouseName","WAREHOUSE_NAME"},
                    {"AutoOrderName","AUTO_ORDER"},
                    {"CriticalStockQuantity","CRITICAL_STOCK_QUANTITY"},
                    {"MinStockQuantity","MIN_STOCK_QUANTITY"},
                    {"MaxStockQuantity","MAX_STOCK_QUANTITY"},
                    {"AvgDealerPrice","AVG_DEALER_PRICE"},
                    {"MinSaleQuantity","MIN_SALE_QUANTITY"},
                    {"SalePrice","SALE_PRICE"},
                    {"ProfitMarginRatio","PROFIT_MARGIN_RATIO"},
                    {"Weight","WEIGHT"},
                    {"Volume","VOLUME"},
                    {"VatRatio","VAT_RATIO"}
                };
            SetMapper(dMapper);
        }

        public StockCardListModel()
        {
        }

        public int StockCardId { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "StockCard_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        public long? PartId { get; set; }
        [Display(Name = "StockCard_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "StockCard_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public int? PartDealerId { get; set; }
        [Display(Name = "StockCard_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartDealerName { get; set; }
        
        public bool? IsOriginalPart { get; set; }
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
        public decimal CriticalStockQuantity { get; set; }

        [Display(Name = "StockCard_Display_MinStockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal MinStockQuantity { get; set; }

        [Display(Name = "StockCard_Display_MaxStockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal MaxStockQuantity { get; set; }

        [Display(Name = "StockCard_Display_AvgDealerPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal AvgDealerPrice { get; set; }

        [Display(Name = "StockCard_Display_MinSaleQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal MinSaleQuantity { get; set; }

        [Display(Name = "StockCard_Display_SalePrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal SalePrice { get; set; }

        [Display(Name = "StockCard_Display_ProfitMarginRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ProfitMarginRatio { get; set; }

        [Display(Name = "StockCard_Display_Weight", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Weight { get; set; }

        [Display(Name = "StockCard_Display_Volume", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Volume { get; set; }

        [Display(Name = "StockCard_Display_VatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal VatRatio { get; set; }
    }
}
