using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.DealerGuaranteeRatio
{
    public class DealerGuaranteeRatioIndexViewModelValidator : AbstractValidator<DealerGuaranteeRatioIndexViewModel>
    {
        public DealerGuaranteeRatioIndexViewModelValidator()
        {
            RuleFor(c => c.IdDealer).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.GuaranteeRatio).LessThan(10000).WithLocalizedMessage(() => MessageResource.Validation_IntegerLength,9999.99);
            RuleFor(c => c.GuaranteeRatio).GreaterThanOrEqualTo(0).WithLocalizedMessage(() => MessageResource.Validation_GreaterThanZero);
        }
    }
}
