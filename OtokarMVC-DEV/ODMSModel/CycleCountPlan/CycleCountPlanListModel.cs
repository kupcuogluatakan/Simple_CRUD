using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CycleCountPlan
{
    public class CycleCountPlanListModel : BaseListWithPagingModel
    {
        public CycleCountPlanListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CycleCountPlanId", "ID_CYCLE_COUNT_PLAN"},
                    {"CycleCountId", "ID_CYCLE_COUNT"},
                    {"WarehouseId", "ID_WAREHOUSE"},
                    {"WarehouseName", "WAREHOUSE_NAME"},
                    {"RackId", "ID_RACK"},
                    {"RackName", "RACK_NAME"},
                    {"StockCardId", "ID_STOCK_CARD"},
                    {"StockCardName", "STOCK_CARD_NAME"}
                };
            SetMapper(dMapper);
        }

        public CycleCountPlanListModel()
        {
        }

        //CycleCountPlanId
        public int CycleCountPlanId { get; set; }

        //CycleCountId
        public int CycleCountId { get; set; }

        //Warehouse
        public int WarehouseId { get; set; }
        [Display(Name = "CycleCountPlan_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        //Rack
        public int? RackId { get; set; }
        [Display(Name = "CycleCountPlan_Display_RackName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackName { get; set; }

        //StockCard
        public int? StockCardId { get; set; }
        [Display(Name = "CycleCountPlan_Display_StockCard", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockCardName { get; set; }
    }
}
