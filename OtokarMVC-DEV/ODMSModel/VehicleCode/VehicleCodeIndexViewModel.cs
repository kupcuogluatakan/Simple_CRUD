using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.Validation;
using ODMSModel.ViewModel;
using FluentValidation.Attributes;

namespace ODMSModel.VehicleCode
{
    [Validator(typeof(VehicleCodeIndexViewModelValidator))]
    public class VehicleCodeIndexViewModel : ModelBase
    {
        public VehicleCodeIndexViewModel()
        {
            VehicleCodeName = new MultiLanguageModel();
        }

        public string MultiLanguageContentAsText { get; set; }

        public bool HideFormElements { get; set; }

        [Display(Name = "VehicleCode_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleCode_Display_Kod", ResourceType = typeof(MessageResource))]
        public string VehicleCodeKod { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        public int VehicleTypeId { get; set; }
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleTypeName { get; set; }

        [Display(Name = "VehicleCode_Display_SSID", ResourceType = typeof(MessageResource))]
        public string VehicleCodeSSID { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        private MultiLanguageModel _vehicleCodeName;
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleCode_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel VehicleCodeName { get { return _vehicleCodeName ?? new MultiLanguageModel(); } set { _vehicleCodeName = value; } }

        [Display(Name = "DealerTechnicianGroup_Display_VehicleGroupName", ResourceType = typeof(MessageResource))]
        public string VehicleGroupId { get; set; }
        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(MessageResource))]
        public string ModelName { get; set; }


        [Display(Name = "Appointment_Display_VehicleCode", ResourceType = typeof(MessageResource))]
        public string VehicleCode { get; set; }

    }
}
