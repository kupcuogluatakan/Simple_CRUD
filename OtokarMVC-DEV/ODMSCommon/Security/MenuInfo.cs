using System;
using System.Collections.Generic;

namespace ODMSCommon.Security
{
    [Serializable]
    public class MenuInfo
    {
        public MenuInfo()
        {
            MenuItems = new List<MenuItemInfo>();
        }

        public List<MenuItemInfo> MenuItems { get; set; }
    }
}
