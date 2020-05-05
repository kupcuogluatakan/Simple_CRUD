using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;
using FluentValidation.Attributes;

namespace ODMSModel.AppointmentIndicatorMainCategory
{
    [Validator(typeof(AppointmentIndicatorMainCategoryViewModelValidator))]
    public class AppointmentIndicatorMainCategoryViewModel : ModelBase
    {
        public AppointmentIndicatorMainCategoryViewModel()
        {
            AppointmentIndicatorMainCategoryName = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        public int AppointmentIndicatorMainCategoryId { get; set; }

        [Display(Name = "AppointmentIndicatorMainCategory_Display_MainCode", ResourceType = typeof(MessageResource))]
        public string MainCode { get; set; }

        [Display(Name = "AppointmentIndicatorCategory_Display_Code", ResourceType = typeof(MessageResource))]
        public string Code { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategory_Display_SubCode", ResourceType = typeof(MessageResource))]
        public string SubCode { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategory_Title_IndicatorTypeCode", ResourceType = typeof(MessageResource))]
        public string IndicatorTypeCode { get; set; }

        [Display(Name = "AppointmentIndicatorMainCategory_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "AppointmentIndicatorMainCategory_Display_CanBeUsedInAppointment", ResourceType = typeof(MessageResource))]
        public bool CanBeUsedInAppointment { get; set; }
        [Display(Name = "AppointmentIndicatorMainCategory_Display_CanBeUsedInAppointment", ResourceType = typeof(MessageResource))]
        public string CanBeUsedInAppointmentName { get; set; }

        private MultiLanguageModel _appointmentIndicatorMainCategoryName;
        [Display(Name = "AppointmentIndicatorMainCategory_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel AppointmentIndicatorMainCategoryName { get { return _appointmentIndicatorMainCategoryName ?? new MultiLanguageModel(); } set { _appointmentIndicatorMainCategoryName = value; } }
    }
}
