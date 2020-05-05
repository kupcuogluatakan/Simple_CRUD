using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSModel.ListModel;

namespace ODMSModel.AppointmentIndicatorCategory
{
    public class AppointmentIndicatorCategoryListModel : BaseListWithPagingModel
    {
        public AppointmentIndicatorCategoryListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"Code", "CODE"},
                    {"AdminDesc", "ADMIN_DESC"},
                    {"AppointmentIndicatorCategoryName","AICL.DESCRIPTION"},
                    {"IsActiveName", "IS_ACTIVE"},
                    {"AppointmentIndicatorMainCategoryName","AIMCL.DESCRIPTION"}
                };
            SetMapper(dMapper);

        }
        public AppointmentIndicatorCategoryListModel()
        {
        }

        public int AppointmentIndicatorCategoryId { get; set; }
        
        public int? AppointmentIndicatorMainCategoryId { get; set; }
        [Display(Name = "AppointmentIndicatorCategory_Display_MainCategoryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentIndicatorMainCategoryName { get; set; }

        [Display(Name = "AppointmentIndicatorCategory_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Code { get; set; }

        [Display(Name = "AppointmentIndicatorCategory_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }
        
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "AppointmentIndicatorCategory_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentIndicatorCategoryName { get; set; }
    }
}
