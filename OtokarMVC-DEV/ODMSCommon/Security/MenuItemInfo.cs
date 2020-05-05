using System.Collections.Generic;

namespace ODMSCommon.Security
{
    public class MenuItemInfo
    {
        public MenuItemInfo()
        {
            Children = new List<MenuItemInfo>();
        }
        public int MenuItemId { get; set; }
        public int? MenuItemParentId { get; set; }
        public int OrderNo { get; set; }
        public List<MenuItemInfo> Children { get; set; }
        public string Text { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
}
