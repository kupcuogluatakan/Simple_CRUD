using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Shared
{
    public class PermissionListModel : BaseListWithPagingModel
    {
        public PermissionListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"PermissionId", "PERMISSION_ID"},
                     {"AdminDesc", "PERMISSION_ADMIN_DESC"},
                     {"PermissionCode", "PERMISSION_CODE"},
                     {"Status", "STATUS"},
                     {"PermissionName", "PERMISSION_NAME"}
                 };
            SetMapper(dMapper);

        }

        public PermissionListModel()
        { }

        public int PermissionId { get; set; }
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }
        [Display(Name = "Permission_Display_PermissionCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PermissionCode { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string Status { get; set; }
        [Display(Name = "Permission_Display_PermissionName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]  
        public string PermissionName { get; set; }

    }
}
