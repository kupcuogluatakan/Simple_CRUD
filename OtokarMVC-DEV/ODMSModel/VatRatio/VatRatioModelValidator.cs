using System.Text.RegularExpressions;
using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.VatRatio
{
    public class VatRatioModelValidator : AbstractValidator<VatRatioModel>
    {
        public VatRatioModelValidator()
        {
            RuleFor(c => c.InvoiceLabel).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.InvoiceLabel).Length(0,2).WithMessage(MessageResource.Validation_Length, 2);
            RuleFor(c => c.VatRatio).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.VatRatio).LessThanOrEqualTo(100).WithMessage(MessageResource.Validation_VatRatioMaximumValue);
            RuleFor(c => c.VatRatio).Must(
                c =>
                    Regex.Match(c.ToString(), @"^(0*100{1,1}\,?((?<=\,)0*)?%?$)|(^0*\d{0,2}\,?((?<=\,)\d*)?%?)$")
                        .Success);
        }
    }
}
