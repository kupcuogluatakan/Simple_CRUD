using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.Bank
{
    [Validator(typeof(BankDetailModelValidator))]
    public class BankDetailModel : ModelBase
    {
        public int Id { get; set; }

        [Display(Name = "Bank_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Code { get; set; }

        [Display(Name = "Bank_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Name { get; set; }

        public bool HideFormElements { get; set; }
    }
}
