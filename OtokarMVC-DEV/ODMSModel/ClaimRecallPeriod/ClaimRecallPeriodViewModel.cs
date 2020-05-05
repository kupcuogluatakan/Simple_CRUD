using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.ClaimRecallPeriod
{
    [Validator(typeof(ClaimRecallPeriodViewModelValidator))]
    public class ClaimRecallPeriodViewModel : ModelBase
    {
        public ClaimRecallPeriodViewModel()
        {
        }

        [Display(Name = "ClaimRecallPeriod_Display_ClaimRecallPeriodId", ResourceType = typeof(MessageResource))]
        public int ClaimRecallPeriodId { get; set; }

        [Display(Name = "ClaimRecallPeriod_Display_ValidLastDay", ResourceType = typeof(MessageResource))]
        public DateTime? ValidLastDay { get; set; }

        [Display(Name = "ClaimRecallPeriod_Display_ShipFirstDay", ResourceType = typeof(MessageResource))]
        public DateTime? ShipFirstDay { get; set; }

        [Display(Name = "ClaimRecallPeriod_Display_ShipLastDay", ResourceType = typeof(MessageResource))]
        public DateTime? ShipLastDay { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
