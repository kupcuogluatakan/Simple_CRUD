using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.PrivateMessage
{
    [Validator(typeof(PrivateMessageModelValidator))]
    public class PrivateMessageModel:ModelBase
    {
        public Int64 MessageId { get; set; }
        [Display(Name="PrivateMessage_Display_Title",ResourceType = typeof(MessageResource))]
        public string Title { get; set; }
        [Display(Name = "PrivateMessage_Display_Message", ResourceType = typeof(MessageResource))]
        public string Message { get; set; }
        [Display(Name = "PrivateMessage_Display_Sender", ResourceType = typeof(MessageResource))]
        public int SenderId { get; set; }
        [Display(Name = "PrivateMessage_Display_Reciever", ResourceType = typeof(MessageResource))]
        public int RecieverId { get; set; }
        [Display(Name = "PrivateMessage_Display_Sender", ResourceType = typeof(MessageResource))]
        public string Sender { get; set; }
        [Display(Name = "PrivateMessage_Display_Reciever", ResourceType = typeof(MessageResource))]
        public string Reciever { get; set; }
        [Display(Name = "PrivateMessage_Display_IsUrgent", ResourceType = typeof(MessageResource))]
        public bool IsUrgent { get; set; }
    }
}
