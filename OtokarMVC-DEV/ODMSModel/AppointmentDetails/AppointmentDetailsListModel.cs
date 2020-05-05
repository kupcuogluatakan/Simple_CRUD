using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.AppointmentDetails
{
    public class AppointmentDetailsListModel:BaseListWithPagingModel
    {
        public AppointmentDetailsListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"AppointmentIndicatorId", "APPOINTMENT_INDICATOR_ID"},
                     {"MainCategoryName", "APPOINTMENT_INDICATOR_MAIN_CATEGORY_ID"},
                     {"CategoryName", "APPOINTMENT_INDICATOR_CATEGORY_ID"},
                     {"SubCategoryName", "APPOINTMENT_INDICATOR_SUB_CATEGORY_ID"},
                     {"FailureCode","AIFC.CODE"},
                     {"IndicType","ITC.INDICATOR_TYPE_NAME"}

                 };
            SetMapper(dMapper);

        }

        public AppointmentDetailsListModel(){}

        [Display(Name = "AppointmentDetails_Display_Appointment", ResourceType = typeof(MessageResource))]
        public int AppointmentId { get; set; }
        [Display(Name = "AppointmentDetails_Display_Appointment", ResourceType = typeof(MessageResource))]
        public int AppointmentIndicatorId { get; set; }
        [Display(Name = "Global_Display_MainCategory", ResourceType = typeof(MessageResource))]
        public int MainCategoryId { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public int CategoryId { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public int SubCategoryId { get; set; }
        [Display(Name = "Global_Display_TotalPrice", ResourceType = typeof(MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Global_Display_MainCategory", ResourceType = typeof(MessageResource))]
        public string MainCategoryName { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public string CategoryName { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public string SubCategoryName { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public string FailureCode { get; set; }
        [Display(Name = "AppointmentDetails_Display_IndicType", ResourceType = typeof(MessageResource))]
        public string IndicType { get; set; }
        public string IndicTypeValue { get; set; }
    }
}
