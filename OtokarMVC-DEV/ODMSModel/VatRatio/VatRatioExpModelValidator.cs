using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.VatRatio
{
    public class VatRatioExpModelValidator : AbstractValidator<VatRatioExpModel>
    {
        public VatRatioExpModelValidator()
        {
            RuleFor(c => c.VatRatio).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CountryId).NotEmpty().WithMessage(MessageResource.Validation_Required);
        }
    }
}
