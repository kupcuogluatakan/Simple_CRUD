using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CycleCount
{
    public class CycleCountViewModelValidator : AbstractValidator<CycleCountViewModel>
    {
        public CycleCountViewModelValidator()
        {
            RuleFor(c => c.CycleCountName)
                .Length(0, 50)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.CycleCountName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
