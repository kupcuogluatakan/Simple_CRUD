using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.DomainModel
{
    [Serializable]
    public class MenuItemModel
    {
        public MenuItemModel()
        {
            Children = new List<MenuItemModel>();
        }
        public int MenuItemId { get; set; }
        public int MenuItemParentId { get; set; }
        public int OrderNo { get; set; }
        public List<MenuItemModel> Children { get; set; }
        public string Text { get; set; }
        public string TooltipText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }

       
    }
}
