using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Shared
{
    public class RoleListModel : BaseListWithPagingModel
    {
        public RoleListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"RoleId", "ROLE_TYPE_ID"},
                     {"IsSystemRole", "IS_SYSTEM_ROLE"},
                     {"AdminDesc","ADMIN_DESC"},
                     {"Status", "STATUS"},
                     {"RoleTypeName", "ROLE_TYPE_NAME"},
                     {"IsActiveString", "IS_ACTIVE_STRING"}
                 };
            SetMapper(dMapper);

        }

        public RoleListModel()
        { }

        public int RoleId { get; set; }
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Status { get; set; }
        [Display(Name = "Role_Display_RoleTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RoleTypeName { get; set; }
        public int? IsSystemRole { get; set; }
        [Display(Name = "Role_Display_IsSystemRole", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsSystemRoleName { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsActive { get; set; }
    }
}
