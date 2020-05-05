using ODMSCommon.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.WebServiceErrorDisplay
{
    public class WebServiceErrorDisplayViewModel : ModelBase
    {
        [Display(Name = "WebServiceError_Display_LogId", ResourceType = typeof(MessageResource))]
        public int? ServiceLogId { get; set; }

        [Display(Name = "WebServiceError_Display_ServiceName", ResourceType = typeof(MessageResource))]
        public string ServiceName { get; set; }

        [Display(Name = "WebServiceError_Display_Response", ResourceType = typeof(MessageResource))]
        public string Response { get; set; }

        [Display(Name = "WebServiceError_Display_RequestParams", ResourceType = typeof(MessageResource))]
        public string RequestParams { get; set; }

        [Display(Name = "WebServiceError_Display_CallStartDate", ResourceType = typeof(MessageResource))]
        public DateTime? CallStartDate { get; set; }

        [Display(Name = "WebServiceError_Display_CallFinishDate", ResourceType = typeof(MessageResource))]
        public DateTime? CallFinishDate { get; set; }

        [Display(Name = "WebServiceError_Display_ErrorContentXml", ResourceType = typeof(MessageResource))]
        public string ErrorContentXml { get; set; }

        [Display(Name = "WebServiceError_Display_IsManuelCall", ResourceType = typeof(MessageResource))]
        public bool IsManuelCall { get; set; }

        [Display(Name = "WebServiceError_Display_ManuelCallUser", ResourceType = typeof(MessageResource))]
        public string ManuelCallUser { get; set; }

        public string Value { get; set; }
    }

    public enum XmlErrorType
    {
        Response,
        RequestParams,
        ErrorContentXml
    }
}
