using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.ScrapDetail
{
    public class ScrapDetailListModel : BaseListWithPagingModel
    {
        public int ScrapId { get; set; }

        [Display(Name = "Scrap_Display_Barcode", ResourceType = typeof(MessageResource))]
        public string Barcode { get; set; }

        public int? DealerId { get; set; }

        [Display(Name = "Global_Display_Select", ResourceType = typeof(MessageResource))]
        public int ScrapDetailId { get; set; }

        [Display(Name = "Scrap_Display_PartName", ResourceType = typeof(MessageResource))]
        public int? PartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "Scrap_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "Scrap_Display_StockTypeName", ResourceType = typeof(MessageResource))]
        public int? StockTypeId { get; set; }
        [Display(Name = "Scrap_Display_StockTypeName", ResourceType = typeof(MessageResource))]
        public string StockTypeName { get; set; }

        [Display(Name = "Scrap_Display_WarehouseName", ResourceType = typeof(MessageResource))]
        public int? WarehouseId { get; set; }
        [Display(Name = "Scrap_Display_WarehouseName", ResourceType = typeof(MessageResource))]
        public string WarehouseName { get; set; }

        [Display(Name = "Scrap_Display_RackName", ResourceType = typeof(MessageResource))]
        public int? RackId { get; set; }
        [Display(Name = "Scrap_Display_RackName", ResourceType = typeof(MessageResource))]
        public string RackName { get; set; }

        [Display(Name = "Scrap_Display_StockQuantity", ResourceType = typeof(MessageResource))]
        public decimal StockQuantity { get; set; }

        [Display(Name = "Scrap_Display_Quantity", ResourceType = typeof(MessageResource))]
        public string Quantity { get; set; }

        public int? ConfirmIdStockTransaction { get; set; }

        public int? CancelIdStockTransaction { get; set; }

        public int? ConfirmUserId { get; set; }
        [Display(Name = "Scrap_Display_ConfirmUserName", ResourceType = typeof(MessageResource))]
        public string ConfirmUserName { get; set; }

        public int? CancelUserId { get; set; }
        [Display(Name = "Scrap_Display_CancelUserName", ResourceType = typeof(MessageResource))]
        public string CancelUserName { get; set; }

        [Display(Name = "Scrap_Display_Unit", ResourceType = typeof(MessageResource))]
        public string Unit { get; set; }

        public bool IsNew { get; set; }

        public ScrapDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"PartId", "SPL.PART_NAME"},
                     {"StockTypeId", "STL.MAINT_NAME"},
                     {"WarehouseId", "W.WAREHOUSE_NAME"},
                     {"RackId", "R.RACK_NAME"},
                     {"StockQuantity", "20"},
                     {"ConfirmUserName", "COU.CUSTOMER_NAME"},
                     {"CancelUserName", "CAU.CUSTOMER_NAME"},
                     {"Unit", "SP.UNIT_LOOKVAL"}
                 };
            SetMapper(dMapper);
        }

        public ScrapDetailListModel() { }
    }
}
