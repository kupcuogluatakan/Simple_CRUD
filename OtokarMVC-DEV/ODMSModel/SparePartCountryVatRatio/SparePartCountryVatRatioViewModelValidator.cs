using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartCountryVatRatio
{
    public class SparePartCountryVatRatioViewModelValidator : AbstractValidator<SparePartCountryVatRatioViewModel>
    {
        public SparePartCountryVatRatioViewModelValidator()
        {
            RuleFor(c => c.VatRatio).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdPart).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdCountry).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
