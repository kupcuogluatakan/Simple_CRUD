using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;


namespace ODMSModel.ObjectSearch
{
    public class AppointmentIndicatorSubCategorySearchListModel : ListModel.BaseListWithPagingModel
    {
        public AppointmentIndicatorSubCategorySearchListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"MainCategoryCode", "MAIN_CATEGORY_CODE"},
                    {"MainCategoryName", "MAIN_CATEGORY_NAME"},
                    {"CategoryCode", "CATEGORY_CODE"},
                    {"CategoryName", "CATEGORY_NAME"},
                    {"SubCategoryCode", "SUB_CATEGORY_CODE"},
                    {"SubCategoryName", "SUB_CATEGORY_NAME"},
                    {"IndicatorTypeCode", "INDICATOR_TYPE_CODE"}
                };
            SetMapper(dMapper);
        }

        public AppointmentIndicatorSubCategorySearchListModel()
        {
        }
        
        public int MainCategoryId { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_MainCode", ResourceType = typeof(MessageResource))]
        public string MainCategoryCode { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_Name", ResourceType = typeof(MessageResource))]
        public string MainCategoryName { get; set; }

        public int CategoryId { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_Code", ResourceType = typeof(MessageResource))]
        public string CategoryCode { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_Name", ResourceType = typeof(MessageResource))]
        public string CategoryName { get; set; }

        public int SubCategoryId { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_SubCode", ResourceType = typeof(MessageResource))]
        public string SubCategoryCode { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_SubName", ResourceType = typeof(MessageResource))]
        public string SubCategoryName { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategorySearch_Display_IndicatorTypeCode", ResourceType = typeof(MessageResource))]
        public string IndicatorTypeCode { get; set; }
    }
}
