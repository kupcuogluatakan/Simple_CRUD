using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CycleCountStockDiff
{
    public class CycleCountStockDiffListModel : BaseListWithPagingModel
    {
        public CycleCountStockDiffListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CycleCountStockDiffId", "ID_CYCLE_COUNT_PLAN"},
                    {"CycleCountId", "ID_CYCLE_COUNT"},
                    {"WarehouseId", "ID_WAREHOUSE"},
                    {"WarehouseName", "WAREHOUSE_NAME"},
                    {"StockCardId", "ID_STOCK_CARD"},
                    {"StockCardName", "STOCK_CARD_NAME"},
                    {"StockTypeId", "ID_STOCK_TYPE"},
                    {"StockTypeName", "STOCK_TYPE_NAME"}
                };
            SetMapper(dMapper);
        }

        public CycleCountStockDiffListModel()
        {
        }

        public CycleCountResult.CycleCountResultListModel ResultListModel { get; set; }
        public CycleCountStockDiff.CycleCountDiffDetailListModel DiffDetailListModel { get; set; }

        //CycleCountStockDiffId
        public int CycleCountStockDiffId { get; set; }

        //CycleCountId
        public int CycleCountId { get; set; }

        //Warehouse
        public int WarehouseId { get; set; }
        [Display(Name = "CycleCountStockDiff_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }
        
        //StockCard
        public int? StockCardId { get; set; }
        [Display(Name = "CycleCountStockDiff_Display_StockCard", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockCardName { get; set; }
        
        [Display(Name = "CycleCountStockDiff_Display_BeforeCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BeforeCount { get; set; }

        [Display(Name = "CycleCountStockDiff_Display_AfterCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal AfterCount { get; set; }

        [Display(Name = "CycleCountResult_Display_OverStock", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal OverStockPrice { get; set; }

        [Display(Name = "CycleCountResult_Display_ShortageStockPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ShortageStockPrice { get; set; }

        [Display(Name = "CycleCountResult_Display_CycleStockPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CycleStockPrice { get; set; }
    }
}
