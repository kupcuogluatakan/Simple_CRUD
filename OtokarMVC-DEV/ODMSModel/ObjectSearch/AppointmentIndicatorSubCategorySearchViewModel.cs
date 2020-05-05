using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;

namespace ODMSModel.ObjectSearch
{
    public class AppointmentIndicatorSubCategorySearchViewModel : ObjectSearchModel
    {
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_MainCode", ResourceType = typeof(MessageResource))]
        public string MainCategoryCode { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_MainName", ResourceType = typeof(MessageResource))]
        public string MainCategoryName { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_Code", ResourceType = typeof(MessageResource))]
        public string CategoryCode { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_Name", ResourceType = typeof(MessageResource))]
        public string CategoryName { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_SubCode", ResourceType = typeof(MessageResource))]
        public string SubCategoryCode { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_SubName", ResourceType = typeof(MessageResource))]
        public string SubCategoryName { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_IndicatorTypeCode", ResourceType = typeof(MessageResource))]
        public string IndicatorTypeCode { get; set; }
    }
}
