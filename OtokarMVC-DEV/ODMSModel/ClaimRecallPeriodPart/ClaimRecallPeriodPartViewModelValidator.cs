using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.ClaimRecallPeriodPart
{
    public class ClaimRecallPeriodPartViewModelValidator : AbstractValidator<ClaimRecallPeriodPartViewModel>
    {
        public ClaimRecallPeriodPartViewModelValidator()
        {
            RuleFor(c => c.PartId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

        }
    }
}
