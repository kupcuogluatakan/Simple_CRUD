using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.DomainModel
{
    [Serializable]
    public class MenuModel
    {
        public MenuModel()
        {
            MenuItems = new List<MenuItemModel>();
        }

        public List<MenuItemModel> MenuItems { get; set; }     
    }
}
