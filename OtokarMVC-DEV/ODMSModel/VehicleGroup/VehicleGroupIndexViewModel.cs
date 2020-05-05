using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.Validation;
using ODMSModel.ViewModel;
using FluentValidation.Attributes;

namespace ODMSModel.VehicleGroup
{
    [Validator(typeof(VehicleGroupIndexViewModelValidator))]
    public class VehicleGroupIndexViewModel : ModelBase
    {
        public VehicleGroupIndexViewModel()
        {
            VehicleGroupName = new MultiLanguageModel();
        }
        public string MultiLanguageContentAsText { get; set; }

        public bool HideFormElements { get; set; }

        public int VehicleGroupId { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        private MultiLanguageModel _vehicleGroupName;
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel VehicleGroupName { get { return _vehicleGroupName ?? new MultiLanguageModel(); } set { _vehicleGroupName = value; } }

        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string GroupName { get; set; }
    }
}
