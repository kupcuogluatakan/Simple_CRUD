using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockLocation
{
    public class StockLocationListModel : BaseListWithPagingModel
    {
        public StockLocationListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"DealerName", "DEALER_SHRT_NAME"},
                    {"PartCode","PART_CODE"},
                    {"PartName","PART_NAME"},
                    {"WarehouseName","WAREHOUSE_NAME"},
                    {"WarehouseCode","WAREHOUSE_CODE"},
                    {"RackCode","RACK_CODE"},
                    {"RackName","RACK_NAME"},
                    {"Quantity","QUANTITY"},
                    {"Unit","UNIT_LOOKVAL"}
                };
            SetMapper(dMapper);
        }

        public StockLocationListModel()
        {
        }

        [Display(Name = "StockLocation_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }
        [Display(Name = "StockLocation_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "StockLocation_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdPart{ get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "StockLocation_Display_Warehouse", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdWarehouse { get; set; }

        [Display(Name = "StockLocation_Display_Warehouse", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }

        [Display(Name = "StockLocation_Display_Rack", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdRack { get; set; }

        [Display(Name = "StockLocation_Display_Rack", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackCode { get; set; }
        public string RackName { get; set; }

        [Display(Name = "StockLocation_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Quantity { get; set; }

        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Unit { get; set; }
    }
}
