using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSModel.CycleCount;

namespace ODMSModel.CycleCountPlan
{
    [Validator(typeof(CycleCountPlanViewModelValidator))]
    public class CycleCountPlanViewModel : ModelBase
    {
        public CycleCountPlanViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        //CycleCountPlanId
        public int CycleCountPlanId { get; set; }

        //CycleCountId
        public int CycleCountId { get; set; }

        //Warehouse
        public int? WarehouseId { get; set; }
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

        public LockType Type { get; set; }

        public int PartId { get; set; }


    }
}
