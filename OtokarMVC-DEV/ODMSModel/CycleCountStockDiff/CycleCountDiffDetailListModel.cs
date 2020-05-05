using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CycleCountStockDiff
{
    public class CycleCountDiffDetailListModel : BaseListWithPagingModel
    {
        public CycleCountDiffDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
        }

        public CycleCountDiffDetailListModel()
        {
        }
        
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

        [Display(Name = "CycleCountStockDiff_Display_DifferenceCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal DifferenceCount { get; set; }

        [Display(Name = "CycleCountStockDiff_Display_TransferedCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TransferedCount { get; set; }
    }
}
