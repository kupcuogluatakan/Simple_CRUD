using System.ComponentModel.DataAnnotations;

namespace ODMSModel.SystemAdministration
{
    public class SystemAdministrationLoginModel : ModelBase
    {
        [Display(Name = "SystemAdministration_Display_UserCode", Prompt = "SystemAdministration_Display_UserCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UserName { get; set; }
        [Display(Name = "SystemAdministration_Display_Password", Prompt ="SystemAdministration_Display_Password", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "SystemAdministration_Display_Rememberme", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool RememberMe { get; set; }
        [Display(Name = "SystemAdministration_Display_Login", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Login { get; set; }

        [Display(Name = "Resimde gösterilen sayıların toplamını giriniz")]
        public string Captcha { get; set; }
    }
}
