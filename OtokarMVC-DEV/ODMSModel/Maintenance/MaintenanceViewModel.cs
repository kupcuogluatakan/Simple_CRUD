using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.Validation;
using ODMSModel.ViewModel;

namespace ODMSModel.Maintenance
{
    [Validator(typeof(MaintenanceViewModelValidator))]
    public class MaintenanceViewModel : ModelBase
    {
        public MaintenanceViewModel()
        {
            MaintName = new MultiLanguageModel();
        }

        public string MultiLanguageContentAsText { get; set; }

        public bool HideFormElements { get; set; }

        public int MaintId { get; set; }
        public string _MaintId { get; set; }
        private MultiLanguageModel _maintName;
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel MaintName { get { return _maintName ?? new MultiLanguageModel(); } set { _maintName = value; } }

        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(MessageResource))]
        public string MaintNameSearch { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public int? VehicleTypeId { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleTypeName { get; set; }
        public string VehicleModelName { get; set; }

        [Display(Name = "Vehicle_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }

        [Display(Name = "Maintenance_Display_Month", ResourceType = typeof(MessageResource))]
        public int? MaintMonth { get; set; }

        public string MaintTypeId { get; set; }
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Maintenance_Display_Type", ResourceType = typeof(MessageResource))]
        public string MaintTypeName { get; set; }

        [Display(Name = "Maintenance_Display_Km", ResourceType = typeof(MessageResource))]
        public int? MaintKM { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [Display(Name = "Global_Display_MainCategory", ResourceType = typeof(MessageResource))]
        public int? MainCategoryId { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public int? CategoryId { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public int? SubCategoryId { get; set; }
        [Display(Name = "Global_Display_MainCategory", ResourceType = typeof(MessageResource))]
        public string MainCategoryName { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public string CategoryName { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public string SubCategoryName { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public int? FailureCodeId { get; set; }

        public string FailureCodeName { get; set; }
    }
}
