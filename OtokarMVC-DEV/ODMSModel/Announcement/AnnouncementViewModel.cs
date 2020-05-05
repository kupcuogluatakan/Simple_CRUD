using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.Announcement
{
    [Validator(typeof (AnnouncementViewModelValidator))]
    public class AnnouncementViewModel : ModelBase
    {
        public AnnouncementViewModel()
        {
        }

        public bool HideFormElements { get; set; }

        public int AnnouncementId { get; set; }

        public int DocId { get; set; }
        [Display(Name = "Announcement_Display_DocName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DocName { get; set; }

        [Display(Name = "Announcement_Display_Title", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Title { get; set; }
        [Display(Name = "Announcement_Display_Body", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Body { get; set; }

        [Display(Name = "Announcement_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Announcement_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public new int IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        public bool IsUrgent { get; set; }
        [Display(Name = "Announcement_Display_IsUrgentName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsUrgentName { get; set; }

        public bool SendMail { get; set; }
        [Display(Name = "Announcement_Display_SendMailName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SendMailName { get; set; }

        [Display(Name = "Announcement_Display_PublishDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PublishDate { get; set; }

        public int? PublishUser { get; set; }
        [Display(Name = "Announcement_Display_PublishUserName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PublishUserName { get; set; }

        public bool IsSlide { get; set; }
        [Display(Name = "Announcement_Display_IsSlideName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsSlideName { get; set; }

    }
}
