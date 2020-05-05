using System.Collections.Generic;

namespace ODMSModel.Menu
{
    public class MenuPlacementViewModel : ModelBase
    {
        public int MenuId { get; set; }

        public string MenuName { get; set; }

        public IEnumerable<string> SubMenus { get; set; }
        public List<System.Web.Mvc.SelectListItem> DefinedSubMenuList;
        //public string SubMenuSplitted
        //{
        //    get
        //    {
        //        if (SubMenus == null || !SubMenus.Any())
        //            return string.Empty;
        //        string retVal = string.Empty;
        //        foreach (var item in SubMenus)
        //        {
        //            retVal += "," + item;
        //        }
        //        return retVal.Substring(1, retVal.Length - 1);
        //    }
        //}

        public string SubMenuString { get; set; }
    }
}
