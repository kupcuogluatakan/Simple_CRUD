using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.GuaranteeAuthorityGroup
{
    [Validator(typeof(GuaranteeAuthorityGroupViewModelValidator))]
    public class GuaranteeAuthorityGroupViewModel:ModelBase
    {
        [Display(Name = "GuaranteeAuthorityGroup_Display_GroupName", ResourceType = typeof(MessageResource))]
        public int GroupId { get; set; }
        [Display(Name = "GuaranteeAuthorityGroup_Display_GroupName", ResourceType = typeof(MessageResource))]
        public string GroupName { get; set; }
        [Display(Name = "GuaranteeAuthorityGroup_Display_MailList", ResourceType = typeof(MessageResource))]
        public string MailList { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveString { get; set; }
    }
}
