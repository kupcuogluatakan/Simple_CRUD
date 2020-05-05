using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;

namespace ODMSModel.ListModel
{
    public class MenuListModel : BaseListWithPagingModel
    {
        public MenuListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"MenuId", "MENU_ID"},
                     {"LinkName", "LINK_NAME"},
                     {"Status", "STATUS"},
                     {"OrderNo", "ORDER_NO"},
                     {"MenuText", "MENU_TEXT"},
                     {"PermissionId", "PERMISSION_ID"}
                 };
            SetMapper(dMapper);

        }

        public MenuListModel()
        { }

        public int MenuId { get; set; }
        public int? PermissionId { get; set; }

        [Display(Name = "Menu_Display_LinkName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LinkName { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Status { get; set; }
        [Display(Name = "Menu_Display_OrderNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int OrderNo { get; set; }
        [Display(Name = "Menu_Display_MenuText", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MenuText { get; set; }
    }
}
