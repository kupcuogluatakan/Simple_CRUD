using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSModel.Validation;

namespace ODMSModel.EducationDates
{
    [Validator(typeof(EducationDatesViewModelValidator))]
    public class EducationDatesViewModel : ModelBase
    {
        public EducationDatesViewModel()
        {
        }

        public bool HideFormElements { get; set; }

        public bool IsRequestRoot { get; set; }

        [Display(Name = "Education_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationCode { get; set; }

        public int RowNumber { get; set; }


        [Display(Name = "EducationDates_Display_Date", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducaitonTime { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "EducationDates_Display_Date", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EducationTimeDT { get; set; }


        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "EducationDates_Display_Place", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationPlace { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "EducationDates_Display_Instructor", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Instructor { get; set; }

        [Display(Name = "EducationDates_Display_Note", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Notes { get; set; }

        [Display(Name = "EducationDates_Display_Max", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? MaximumAtt { get; set; }

        [Display(Name = "EducationDates_Display_Min", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? MinimumAtt { get; set; }
    }
}
