using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockCard
{
    public class StockCardSearchListModel : BaseListWithPagingModel
    {
        public StockCardSearchListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartCode","PART_CODE"},
                    {"PartName","PART_NAME"},
                    {"DealerName", "DEALER_NAME"},
                    {"StockTypeName", "MAINT_NAME"},
                    {"StockQuantity", "9"},
                    {"AveragePrice", "AVERAGE_PRICE"},
                    {"SalePrice", "SALE_PRICE"},
                    {"StockQuantityString", "STOCK_QUANTITY"}
                };
            SetMapper(dMapper);
        }

        public StockCardSearchListModel()
        {
        }

        public int? PartId { get; set; }
        [Display(Name = "StockCard_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "StockCard_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "StockCard_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCodeList { get; set; }

        public bool? IsHq { get; set; }
        public int? CurrentDealerId { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? DealerId { get; set; }
        [Display(Name = "StockCard_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "StockCard_Display_StockTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StockTypeId { get; set; }
        
        [Display(Name = "StockCard_Display_StockLocation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int StockLocationId { get; set; }
        [Display(Name = "StockCard_Display_StockTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }
        [Display(Name = "StockCard_Display_StockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? StockQuantity { get; set; }
        [Display(Name = "StockCard_Display_StockTotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? StockTotalPrice { get; set; }
        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UnitName { get; set; }
        [Display(Name = "StockCard_Display_StockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockQuantityString { get; set; }
        [Display(Name = "StockCard_Display_AveragePrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? AveragePrice { get; set; }
        [Display(Name = "StockCard_Display_SalePrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? SalePrice { get; set; }
        [Display(Name = "DealerSaleSparepart_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdsForCentral { get; set; }

        public string DealerRegionIds { get; set; }
        public int DealerRegionType { get; set; }

        public bool UseExcel { get; set; }
    }
}
