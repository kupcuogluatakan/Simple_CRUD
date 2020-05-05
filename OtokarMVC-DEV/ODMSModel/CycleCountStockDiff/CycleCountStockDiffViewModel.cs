using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.CycleCountStockDiff
{
    [Validator(typeof(CycleCountStockDiffViewModelValidator))]
    public class CycleCountStockDiffViewModel : ModelBase
    {
        public CycleCountStockDiffViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        //CycleCountStockDiffId
        public int CycleCountStockDiffId { get; set; }

        //CycleCountId
        public int CycleCountId { get; set; }

        //Warehouse
        public int? WarehouseId { get; set; }
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
    }
}
