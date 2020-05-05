using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
namespace ODMSModel.GuaranteeAuthorityLimitations
{
    [Validator(typeof(GuaranteeAuthorityLimitationsViewModelValidator))]
    public class GuaranteeAuthorityLimitationsViewModel:ModelBase
    {
        [Display(Name = "Campaign_Display_ModelKod", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKod { get; set; }
        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelName { get; set; }
        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "Currency_Display_CurrencyName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyName { get; set; }
        [Display(Name = "GuaranteeAuthorityLimitations_Display_Amount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Amount { get; set; }
        [Display(Name = "GuaranteeAuthorityLimitations_Display_CumulativeAmount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CumulativeAmount { get; set; }
    }
}
