using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.VehicleType
{
    [Validator(typeof(VehicleTypeIndexViewModelValidator))]
    public class VehicleTypeIndexViewModel : ModelBase
    {
        public VehicleTypeIndexViewModel()
        {
        }

        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleGroupId { get; set; }

        public bool HideFormElements { get; set; }
        [Display(Name = "Appointment_Display_VehicleType", ResourceType = typeof(MessageResource))]
        public int TypeId { get; set; }

        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string ModelName { get; set; }

        public string ModelKod { get; set; }

        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string TypeName { get; set; }

        [Display(Name = "VehicleModel_Display_SSID", ResourceType = typeof(MessageResource))]
        public string TypeSSID { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

    }
}
