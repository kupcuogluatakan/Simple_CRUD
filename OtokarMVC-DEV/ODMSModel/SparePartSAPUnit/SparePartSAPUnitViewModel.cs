using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.SparePartSAPUnit
{
    [Validator(typeof(SparePartSAPUnitViewModelValidator))]
    public class SparePartSAPUnitViewModel : ModelBase
    {
        public SparePartSAPUnitViewModel()
        {
        }

        public string txtPartId { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }
        [Display(Name = "SparePart_Display_PartCodeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PartId { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_SparePartCode", ResourceType = typeof(MessageResource))]
        public string SparePartCode { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_SparePartName", ResourceType = typeof(MessageResource))]
        public string SparePartName { get; set; }

        public int UnitId { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_UnitName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UnitName { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_ShipQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ShipQuantity { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_StateName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StateName { get; set; }
    }
}
