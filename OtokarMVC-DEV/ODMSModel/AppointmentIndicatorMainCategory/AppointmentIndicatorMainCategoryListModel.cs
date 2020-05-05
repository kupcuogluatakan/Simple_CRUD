using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSModel.ListModel;

namespace ODMSModel.AppointmentIndicatorMainCategory
{
    public class AppointmentIndicatorMainCategoryListModel : BaseListWithPagingModel
    {
        public AppointmentIndicatorMainCategoryListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"MainCode", "MAIN_CODE"},
                    {"AdminDesc", "ADMIN_DESC"},
                    {"AppointmentIndicatorMainCategoryName","DESCRIPTION"},
                    {"IsActiveName", "IS_ACTIVE"}
                };
            SetMapper(dMapper);

        }
        public AppointmentIndicatorMainCategoryListModel()
        {
        }

        public int AppointmentIndicatorMainCategoryId { get; set; }

        [Display(Name = "AppointmentIndicatorMainCategory_Display_MainCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MainCode { get; set; }

        [Display(Name = "AppointmentIndicatorMainCategory_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "AppointmentIndicatorMainCategory_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentIndicatorMainCategoryName { get; set; }
    }
}
