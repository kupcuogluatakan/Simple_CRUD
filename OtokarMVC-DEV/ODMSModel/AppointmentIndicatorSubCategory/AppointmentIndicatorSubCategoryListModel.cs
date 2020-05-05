using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSModel.ListModel;

namespace ODMSModel.AppointmentIndicatorSubCategory
{
    public class AppointmentIndicatorSubCategoryListModel : BaseListWithPagingModel
    {
        public AppointmentIndicatorSubCategoryListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"SubCode", "SUB_CODE"},
                    {"AdminDesc", "ADMIN_DESC"},
                    {"AppointmentIndicatorSubCategoryName","AISCL.DESCRIPTION"},
                    {"IsActiveName", "IS_ACTIVE"},
                    {"AppointmentIndicatorCategoryName","AICL.DESCRIPTION"},
                    {"AppointmentIndicatorMainCategoryName","AIMCL.DESCRIPTION"},
                    {"IsAutoCreateName","IS_AUTO_CREATE"}
                };
            SetMapper(dMapper);

        }
        public AppointmentIndicatorSubCategoryListModel()
        {
        }

        public int? AppointmentIndicatorSubCategoryId { get; set; }

        public int? AppointmentIndicatorMainCategoryId { get; set; }
        [Display(Name = "AppointmentIndicatorCategory_Display_MainCategoryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentIndicatorMainCategoryName { get; set; }

        public int? AppointmentIndicatorCategoryId { get; set; }
        [Display(Name = "AppointmentIndicatorCategory_Display_CategoryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentIndicatorCategoryName { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategory_Display_SubCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SubCode { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategory_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        public bool? IsAutoCreate { get; set; }
        [Display(Name = "AppointmentIndicatorSubCategory_Display_IsAutoCreateName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsAutoCreateName { get; set; }

        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "AppointmentIndicatorSubCategory_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentIndicatorSubCategoryName { get; set; }
    }
}
