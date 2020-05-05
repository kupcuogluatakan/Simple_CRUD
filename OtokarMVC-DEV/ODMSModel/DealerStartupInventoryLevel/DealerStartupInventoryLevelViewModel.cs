using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.DealerStartupInventoryLevel
{
    [Validator(typeof(DealerStartupInventoryLevelViewModelValidator))]
    public class DealerStartupInventoryLevelViewModel : ModelBase
    {
        public string DealerClassCode { get; set; }
        public int? DealerClassId { get; set; }
        [Display(Name = "DealerStartupInventoryLevel_Display_DealerClassName", ResourceType = typeof(MessageResource))]
        public string DealerClassName { get; set; }

        public int? PartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "DealerStartupInventoryLevel_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "SparePart_Display_PartCodeName", ResourceType = typeof(MessageResource))]
        public string PartCodeName { get; set; }

        [Display(Name = "DealerStartupInventoryLevel_Display_Quantity", ResourceType = typeof(MessageResource))]
        public decimal? Quantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_PackageQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? PackageQuantity { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
