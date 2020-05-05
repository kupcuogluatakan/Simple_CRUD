using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockTypeDetail
{
    public class StockTypeDetailListModel : BaseListWithPagingModel
    {
        public StockTypeDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"WarehouseName","WAREHOUSE_NAME"},
                    {"StockTypeName", "STOCK_TYPE_NAME"},
                    {"PartCode", "PART_CODE"},
                    {"PartName", "PART_NAME"},
                    {"UnitName","SP.UNIT_LOOKVAL"},
                    {"TypeQuantity","ISNULL(SUM(STD.QUANTITY),0)"},
                    {"BlockQuantity","ISNULL(SUM(STD.BLOCK_QNTY),0)"},
                    {"ReserveQuantity","ISNULL(SUM(STD.RESERVE_QNTY),0)"},
                    {"OpenQuantity","16"}
                };
            SetMapper(dMapper);
        }

        public StockTypeDetailListModel()
        { 
        }

        //IdPart
        [Display(Name = "StockTypeDetail_Display_IdPart", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdPart { get; set; }

        //IdDealer
        [Display(Name = "StockTypeDetail_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer{ get; set; }

        //IdWarehouse
        public int? IdWarehouse { get; set; }
        [Display(Name = "StockTypeDetail_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        //PartName
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        //PartCode
        [Display(Name = "StockTypeDetail_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        //StockQuantity
        [Display(Name = "StockTypeDetail_Display_StockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? StockQuantity { get; set; }

        //AdminDesc
        [Display(Name = "StockTypeDetail_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        //IdStockType
        [Display(Name = "StockTypeDetail_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdStockType { get; set; }

        [Display(Name = "StockTypeDetail_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }

        //DealerName
        [Display(Name = "StockTypeDetail_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        //Unit
        [Display(Name = "StockTypeDetail_Display_UnitName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UnitName { get; set; }

        //TypeQuantity
        [Display(Name = "StockTypeDetail_Display_TypeQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TypeQuantity { get; set; }

        //BlockQuantity
        [Display(Name = "StockTypeDetail_Display_BlockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BlockQuantity { get; set; }

        //ReserveQuantity
        [Display(Name = "StockTypeDetail_Display_ReserveQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ReserveQuantity { get; set; }

        //OpenQuantity
        [Display(Name = "StockTypeDetail_Display_OpenQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal OpenQuantity { get; set; }

        public bool AllowServiceEquipment { get; set; }
    }
}
