using System.ComponentModel.DataAnnotations;
using ODMSModel.ViewModel;

namespace ODMSModel.Permission
{
    public class PermissionIndexViewModel : ModelBase
    {
        public PermissionIndexViewModel()
        {
        }

        public int PermissionId { get; set; }
        public string MultiLanguageContentAsText { get; set; }

        //AdminDesc
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        //PermissionCode
        [Display(Name = "Permission_Display_PermissionCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PermissionCode { get; set; }

        //PermissionName
        private MultiLanguageModel _permissionName;
        [Display(Name = "Permission_Display_PermissionName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public MultiLanguageModel PermissionName { get { return _permissionName ?? new MultiLanguageModel(); } set { _permissionName = value; } }

        //Status
        [Display(Name = "Global_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }

        public bool IsOtokarScreen { get; set; }
    }
}