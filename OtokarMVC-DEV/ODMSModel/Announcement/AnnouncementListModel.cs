using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Announcement
{
    public class AnnouncementListModel : BaseListWithPagingModel
    {
        public AnnouncementListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"DocName", "DOC_NAME"},
                    {"Title","TITLE"},
                    {"StartDate","START_DATE"},
                    {"EndDate", "END_DATE"},
                    {"IsUrgentName", "IS_URGENT"},
                    {"IsActiveName", "IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public AnnouncementListModel()
        {
        }

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
        
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }
        
        public int? IsUrgent { get; set; }
        [Display(Name = "Announcement_Display_IsUrgentName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsUrgentName { get; set; }

        public int? SendMail { get; set; }
        [Display(Name = "Announcement_Display_SendMailName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SendMailName { get; set; }

        [Display(Name = "Announcement_Display_PublishDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PublishDate { get; set; }

        public int? PublishUser { get; set; }
        [Display(Name = "Announcement_Display_PublishUserName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PublishUserName { get; set; }

        public int SumDealer { get; set; }
        public int SumRole { get; set; }
    }
}
