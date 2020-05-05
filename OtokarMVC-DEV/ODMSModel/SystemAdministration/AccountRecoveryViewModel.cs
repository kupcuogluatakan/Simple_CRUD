using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.SystemAdministration
{
    [Validator(typeof(AccountRecoveryViewModelValidator))]
    public class AccountRecoveryViewModel: ModelBase
    {
        [Display(Name = "SystemAdministration_Display_UserCode", Prompt = "SystemAdministration_Display_UserCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UserName { get; set; }

        [Display(Name = "User_Display_EMail", Prompt = "SystemAdministration_Display_UserCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Email { get; set; }

        [Display(Name = "User_Display_IdentityNo", Prompt = "SystemAdministration_Display_IdentityNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IdentityNo { get; set; }

        public string Password { get; set; }
       
        [Display(Name = "User_Display_Captcha")]
        public string Captcha { get; set; }
    }
}
