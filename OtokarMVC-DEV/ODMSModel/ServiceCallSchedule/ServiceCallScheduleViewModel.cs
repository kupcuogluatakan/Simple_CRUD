using System;
using ODMSCommon.Resources;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.ServiceCallSchedule
{
    [Validator(typeof(ServiceCallScheduleViewModelValidator))]
    public class ServiceCallScheduleViewModel : ModelBase
    {
        public int ServiceId { get; set; }

        [Display(Name = "ServiceCallSchedule_View_Description", ResourceType = typeof(MessageResource))]
        public string ServiceDescription { get; set; }

        [Display(Name = "ServiceCallSchedule_View_InvervalMinute", ResourceType = typeof(MessageResource))]
        public decimal CallIntervalMinute { get; set; }

        [Display(Name = "ServiceCallSchedule_View_LastCallDate", ResourceType = typeof(MessageResource))]
        public string LastCallDate { get; set; }

        [Display(Name = "ServiceCallSchedule_View_NextCallDate", ResourceType = typeof(MessageResource))]
        public DateTime NextCallDate { get; set; }

        [Display(Name = "ServiceCallSchedule_View_LastSuccessCallDate", ResourceType = typeof(MessageResource))]
        public DateTime? LastSuccessCallDate { get; set; }

        public bool IsTriggerService { get; set; }

        [Display(Name = "ServiceCallSchedule_View_IsTriggerService", ResourceType = typeof(MessageResource))]
        public string TriggerServiceName { get; set; }

        [Display(Name = "ServiceCallSchedule_View_IsResponseLogged", ResourceType = typeof(MessageResource))]
        public bool IsResponseLogged { get; set; }
    }
}
