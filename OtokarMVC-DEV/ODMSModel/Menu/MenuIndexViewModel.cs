using System.ComponentModel.DataAnnotations;
using ODMSModel.ViewModel;

namespace ODMSModel.Menu
{
    public class MenuIndexViewModel : ModelBase
    {
        public MenuIndexViewModel()
        {
        }

        public int MenuId { get; set; }
        public string MultiLanguageContentAsText { get; set; }
        public int? PermissionId { get; set; }

        //LinkName = ControllerName/ActionName
        [Display(Name = "Menu_Display_LinkName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LinkName { get; set; }

        //OrderNo
        [Display(Name = "Menu_Display_OrderNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int OrderNo { get; set; }
        
        //MenuText
        private MultiLanguageModel _menuText;
        [Display(Name = "Menu_Display_MenuText", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public MultiLanguageModel MenuText { get { return _menuText ?? new MultiLanguageModel(); } set { _menuText = value; } }

        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
