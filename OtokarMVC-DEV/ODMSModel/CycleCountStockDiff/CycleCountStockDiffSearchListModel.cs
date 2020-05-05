using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CycleCountStockDiff
{
    public class CycleCountStockDiffSearchListModel : BaseListWithPagingModel
    {
        public CycleCountStockDiffSearchListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"StockTypeName", "STOCK_TYPE_ID"},
                    {"DealerId", "ID_DEALER"},
                    {"DealerName", "DEALER_NAME"},
                    {"StockCardId","ID_STOCK_CARD"},
                    {"StockCardName","STOCK_CARD_NAME"},
                    {"PartCode","PART_CODE"},
                    {"PartName","PART_NAME"},
                    {"BeforeCountTotal","BEFORE_COUNT_TOTAL"},
                    {"AfterCountTotal","AFTER_COUNT_TOTAL"},
                    {"DiffCount","DIFF_COUNT_QUANTITY"}
                };
            SetMapper(dMapper);
        }

        public CycleCountStockDiffSearchListModel()
        {
        }

        public int StockTypeId { get; set; }
        [Display(Name = "CycleCountStockDiff_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }

        [Display(Name = "CycleCountStockDiff_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "CycleCountStockDiff_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        public int DealerId { get; set; }
        [Display(Name = "CycleCountStockDiff_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        
        public int? StockCardId { get; set; }
        [Display(Name = "GuaranteeParts_Display_PartNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockCardName { get; set; }

        [Display(Name = "CycleCountStockDiff_Display_BeforeCountTotal", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BeforeCountTotal { get; set; }

        [Display(Name = "CycleCountStockDiff_Display_AfterCountTotal", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal AfterCountTotal { get; set; }

        [Display(Name = "CycleCountStockDiff_Display_DiffTotalCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal DiffCount { get; set; }

        [Display(Name = "CycleCountStockDiff_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "CycleCountStockDiff_Display_IsStockChanged", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsStockChanged { get; set; }

    }
}
