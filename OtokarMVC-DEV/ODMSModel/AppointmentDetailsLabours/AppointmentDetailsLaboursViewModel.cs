using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.AppointmentDetailsLabours
{
    [Validator(typeof(AppointmentDetailsLaboursViewModelValidator))]
    public class AppointmentDetailsLaboursViewModel:ModelBase
    {
        public bool HideElements { get; set; }
        public int AppointmentIndicatorId { get; set; }
        public long AppointmentId { get; set; }
        public int AppointmentIndicatorLabourId { get; set; }
        public string IndicType { get; set; }
        [Display(Name = "LabourDuration_Display_Labour", ResourceType = typeof(MessageResource))]
        public int LabourId { get; set; }
        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(MessageResource))]
        public int? Quantity { get; set; }
        [Display(Name = "LabourDuration_Display_Duration", ResourceType = typeof(MessageResource))]
        public decimal Duration { get; set; }
        public string txtLabourId { get; set; }
    }
}
