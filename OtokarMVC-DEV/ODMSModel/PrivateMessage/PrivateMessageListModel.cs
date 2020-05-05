using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PrivateMessage
{
    public class PrivateMessageListModel:BaseListWithPagingModel
    {
        public PrivateMessageListModel(DataSourceRequest request):base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Title", "TITLE"},
                     {"Message", "MESSAGE"},
                     {"Sender", "SENDER"},
                     {"Reciever", "RECIEVER"},
                     {"IsUrgent", "IS_URGENT"},
                     {"ReadDate", "READ_DATE"},
                     {"CreateDate", "CREATE_DATE"}
                 };
            SetMapper(dMapper);
        }

        public PrivateMessageListModel()
        {
            
        }
        public Int64 MessageId { get; set; }
        
        [Display(Name = "PrivateMessage_Display_Title",ResourceType = typeof(MessageResource))]
        public string Title { get; set; }
         [Display(Name = "PrivateMessage_Display_Message", ResourceType = typeof(MessageResource))]
        public string Message { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
         [Display(Name = "PrivateMessage_Display_Sender", ResourceType = typeof(MessageResource))]
        public string Sender { get; set; }
         [Display(Name = "PrivateMessage_Display_Reciever", ResourceType = typeof(MessageResource))]
        public string Reciever { get; set; }
         [Display(Name = "PrivateMessage_Display_IsUrgent", ResourceType = typeof(MessageResource))]
        public bool IsUrgent { get; set; }
         [Display(Name = "PrivateMessage_Display_ReadDate", ResourceType = typeof(MessageResource))]
        public DateTime? ReadDate { get; set; }
         [Display(Name = "PrivateMessage_Display_CreateDate", ResourceType = typeof(MessageResource))]
        public DateTime CreateDate { get; set; }
         [Display(Name = "PrivateMessage_Display_Type", ResourceType = typeof(MessageResource))]
        public string Type { get; set; }
    }
}
