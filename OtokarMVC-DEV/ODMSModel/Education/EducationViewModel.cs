using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.Validation;
using ODMSModel.ViewModel;

namespace ODMSModel.Education
{
    [Validator(typeof(EducationViewModelValidator))]
    public class EducationViewModel : ModelBase
    {
        public EducationViewModel()
        {
            EducationName = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Education_Display_Code", ResourceType = typeof(MessageResource))]
        public string EducationCode { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Education_Display_Type", ResourceType = typeof(MessageResource))]
        public int? EducationTypeId { get; set; }
        [Display(Name = "Education_Display_Type", ResourceType = typeof(MessageResource))]
        public string EducationTypeName { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Education_Display_DurationDay", ResourceType = typeof(MessageResource))]
        public int EducationDurationDay { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Education_Display_DurationHour", ResourceType = typeof(MessageResource))]
        public int EducationDurationHour { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Education_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModelCode { get; set; }
        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModelName { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Education_Display_Must", ResourceType = typeof(MessageResource))]
        public bool? IsMandatorySearch { get; set; }
        [Display(Name = "Education_Display_Must", ResourceType = typeof(MessageResource))]
        public bool IsMandatory { get; set; }

        private MultiLanguageModel _educationName;
        [Display(Name = "Education_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel EducationName { get { return _educationName ?? new MultiLanguageModel(); } set { _educationName = value; } }

    }
}
