using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.WorkHour
{
    public class WorkHourListModel:BaseListWithPagingModel
    {
        public WorkHourListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"WorkHourId", "ID_WORK_HOUR"},
                     {"ValidityStartDate", "VALIDITY_DATE_START"},
                     {"ValidityEndDate", "VALIDITY_DATE_END"},
                     {"WorkStatusString", "WORK_STAT"},
                     {"Priority", "PRIORITY"},
                     {"AppointmentPeriod", "APPOINTMENT_PERIOD"},
                     {"IsActiveString","IS_ACTIVE"}
                 };
            SetMapper(dMapper);

        }

        public WorkHourListModel()
        {
            // TODO: Complete member initialization
        }

        public int WorkHourId { get; set; }
        [Display(Name = "WorkHour_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime? ValidityStartDate { get; set; }
        [Display(Name = "WorkHour_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? ValidityEndDate { get; set; }
        [Display(Name = "WorkHour_Display_StatusOfWork", ResourceType = typeof(MessageResource))]
        public string WorkStatusString { get; set; }
        [Display(Name = "WorkHour_Display_Priority", ResourceType = typeof(MessageResource))]
        public int? Priority { get; set; }
        [Display(Name = "WorkHour_Display_MeetingInterval", ResourceType = typeof(MessageResource))]
        public int AppointmentPeriod { get; set; }

        public int DealerId { get; set; }

        [Display(Name = "WorkHour_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "WorkHour_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "WorkHour_Display_MeetingInterval", ResourceType = typeof(MessageResource))]
        public int? MeetingInterval { get; set; }

        [Display(Name = "WorkHour_Display_StatusOfWork", ResourceType = typeof(MessageResource))]
        public int? StatusOfWork { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
        [Display(Name = "WorkHour_Display_WorkStartHour", ResourceType = typeof(MessageResource))]
        public TimeSpan WorkStartHour { get; set; }
        [Display(Name = "WorkHour_Display_WorkEndHour", ResourceType = typeof(MessageResource))]
        public TimeSpan WorkEndHour { get; set; }
    }
}
