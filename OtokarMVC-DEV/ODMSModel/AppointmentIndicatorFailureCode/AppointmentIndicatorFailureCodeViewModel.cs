using ODMSCommon.Resources;
using ODMSModel.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.AppointmentIndicatorFailureCode
{
    [Validator(typeof(AppointmentIndicatorFailureCodeViewModelValidator))]
    public class AppointmentIndicatorFailureCodeViewModel : ModelBase
    {

        public AppointmentIndicatorFailureCodeViewModel()
        {
            DescriptionML = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public int IdAppointmentIndicatorFailureCode { get; set; }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public string Code { get; set; }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        private MultiLanguageModel _appointmentIndicatorFailureCodeDescName;

        [Display(Name = "AppointmentIndicatorFailureCode_Display_Description", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel DescriptionML { get { return _appointmentIndicatorFailureCodeDescName ?? new MultiLanguageModel(); } set { _appointmentIndicatorFailureCodeDescName = value; } }

        public DateTime? CreateDate { get; set; }
    }
}
