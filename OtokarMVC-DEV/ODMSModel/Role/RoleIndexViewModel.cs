using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSModel.Validation;
using ODMSModel.ViewModel;


namespace ODMSModel.Role
{
    [Validator(typeof(RoleIndexViewModelValidator))]
    public class RoleIndexViewModel : ModelBase
    {


        public RoleIndexViewModel()
        {
            RoleTypeName = new MultiLanguageModel();
        }
        public string MultiLanguageContentAsText { get; set; }
        public string isValidText { get; set; }
        public int RoleId { get; set; }

        public bool HideFormElements { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        public bool IsSystemRole { get; set; }
        [Display(Name = "Role_Display_IsSystemRole", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsSystemRoleName { get; set; }

        private MultiLanguageModel _roleTypeName;
        [Display(Name = "Role_Display_RoleTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public MultiLanguageModel RoleTypeName { get { return _roleTypeName ?? new MultiLanguageModel(); } set { _roleTypeName = value; } }
    }
}
