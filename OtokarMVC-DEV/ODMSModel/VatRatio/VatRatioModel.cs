using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.VatRatio
{
    [Validator(typeof(VatRatioModelValidator))]
    public class VatRatioModel : ModelBase
    {
        [Display(Name = "Vehicle_Display_InvoiceLabel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceLabel { get; set; }

        [Display(Name = "Vehicle_Display_VatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal VatRatio { get; set; }
    }
}
