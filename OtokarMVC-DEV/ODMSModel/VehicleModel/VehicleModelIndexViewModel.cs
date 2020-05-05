using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.Validation;

namespace ODMSModel.VehicleModel
{
    [Validator(typeof(VehicleModelIndexViewModelValidator))]
    public class VehicleModelIndexViewModel : ModelBase
    {
        public VehicleModelIndexViewModel()
        {
        }

        public bool HideFormElements { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleModel_Display_Code", ResourceType = typeof(MessageResource))]
        public string VehicleModelKod { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModelName { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleModel_Display_SSID", ResourceType = typeof(MessageResource))]
        public string VehicleModelSSID { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleModel_Display_CouponCheck", ResourceType = typeof(MessageResource))]
        public bool IsCouponCheck { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleModel_Display_PDICheck", ResourceType = typeof(MessageResource))]
        public bool IsPDICheck { get; set; }

        public int VehicleGroupId { get; set; }
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof (MessageResource))]
        public string VehicleGroupName { get; set; }

        [Display(Name = "Vehicle_Display_BodyWork_Detailed_Required", ResourceType = typeof(MessageResource))]
        public bool IsBodyWorkDetailCheck { get; set; }
    }
}
