using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.DealerRegion
{
    public class DealerRegionIndexViewModelValidator : AbstractValidator<DealerRegionIndexViewModel>
    {
        public DealerRegionIndexViewModelValidator()
        {
            RuleFor(c => c.DealerRegionName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DealerRegionName)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
        }
    }
}
