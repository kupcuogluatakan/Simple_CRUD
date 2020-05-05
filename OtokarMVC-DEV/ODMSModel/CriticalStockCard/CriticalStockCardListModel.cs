using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CriticalStockCard
{
    public class CriticalStockCardListModel : BaseListWithPagingModel
    {
        public CriticalStockCardListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartCode","PART_CODE"},
                    {"PartName","PART_NAME"},
                    {"DealerName", "DEALER_NAME"},
                    {"CriticalStockQuantity","CRITICAL_STOCK_QUANTITY"},
                    {"ShipQty","SHIP_QUANT"},
                    {"Unit","UNIT_LOOKVAL"}
                };
            SetMapper(dMapper);
        }

        public CriticalStockCardListModel()
        {
        }

        public int StockCardId { get; set; }

        public int? IdDealer { get; set; }
        [Display(Name = "CriticalStockCard_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        public Int64? IdPart { get; set; }

        [Display(Name = "CriticalStockCard_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "CriticalStockCard_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "CriticalStockCard_Display_CriticalStockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? CriticalStockQuantity { get; set; }

        [Display(Name = "CriticalStockCard_Display_PartCodeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public AutoComplete AutoCompleteLabel { get; set; }

        [Display(Name = "CriticalStockCard_Display_ShipQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ShipQty { get; set; }

        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Unit { get; set; }
    }
}
