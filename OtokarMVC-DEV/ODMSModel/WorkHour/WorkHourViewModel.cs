using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon;
using ODMSCommon.Resources;
using FluentValidation.Attributes;
namespace ODMSModel.WorkHour
{
    [Validator(typeof(WorkHourViewModelValidator))]
    public class WorkHourViewModel : ModelBase
    {
        public WorkHourViewModel()
        {
            WeekDays = new List<bool>(7);
            TeaBreaks = new List<TeaBreakModel>();
        }
        [Display(Name = "WorkHour_Display_Priority", ResourceType = typeof(MessageResource))]
        public int? Priority { get; set; }

        public int WorkHourId { get; set; }

        public int DealerId { get; set; }
        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "WorkHour_Display_StartDate", ResourceType = typeof(MessageResource))]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "WorkHour_Display_EndDate", ResourceType = typeof(MessageResource))]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "WorkHour_Display_StatusOfWork", ResourceType = typeof(MessageResource))]
        public WorkStatus StatusOfWork { get; set; }
        [Display(Name = "WorkHour_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }
        [Display(Name = "WorkHour_Display_MeetingInterval", ResourceType = typeof(MessageResource))]
        public int? MeetingInterval { get; set; }

        [Display(Name = "WorkHour_Display_MeetingIntervalStart", ResourceType = typeof(MessageResource))]
        public int? MeetingIntervalStart { get; set; }

        [Display(Name = "WorkHour_Display_MeetingIntervalEnd", ResourceType = typeof(MessageResource))]
        public int? MeetingIntervalEnd { get; set; }

        [Display(Name = "WorkHour_Display_WeekDays", ResourceType = typeof(MessageResource))]
        public List<bool> WeekDays { get; set; }
        [Display(Name = "WorkHour_Display_TeaBreak", ResourceType = typeof(MessageResource))]
        public List<TeaBreakModel> TeaBreaks { get; set; }
        [Display(Name = "WorkHour_Display_WorkStartHour", ResourceType = typeof(MessageResource))]
        public TimeSpan WorkStartHour { get; set; }
        [Display(Name = "WorkHour_Display_WorkEndHour", ResourceType = typeof(MessageResource))]
        public TimeSpan WorkEndHour { get; set; }
        [Display(Name = "WorkHour_Display_LauchBreakStartHour", ResourceType = typeof(MessageResource))]
        public TimeSpan LaunchBreakStartHour { get; set; }
        [Display(Name = "WorkHour_Display_LauchBreakEndHour", ResourceType = typeof(MessageResource))]
        public TimeSpan LaunchBreakEndHour { get; set; }        
        [Display(Name = "User_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool? IsActiveSearch { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "User_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        public enum DayOfWeek
        {
            [StringValue("WorkHour_Display_Monday")]
            Monday = 1,
            [StringValue("WorkHour_Display_Tuesday")]
            Tuesday,
            [StringValue("WorkHour_Display_Wednesday")]
            Wednesday,
            [StringValue("WorkHour_Display_Thursday")]
            Thursday,
            [StringValue("WorkHour_Display_Friday")]
            Friday,
            [StringValue("WorkHour_Display_Saturday")]
            Saturday,
            [StringValue("WorkHour_Display_Sunday")]
            Sunday
        }

        public enum WorkStatus
        {
            [StringValue("WorkHour_StatusOfWork_NotWorks")]
            NotWorks = 0,
            [StringValue("WorkHour_StatusOfWork_Works")]
            Works = 1
        }
    }
}
