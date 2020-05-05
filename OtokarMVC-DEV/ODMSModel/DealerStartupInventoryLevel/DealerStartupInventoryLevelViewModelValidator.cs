using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.DealerStartupInventoryLevel
{
    public class DealerStartupInventoryLevelViewModelValidator : AbstractValidator<DealerStartupInventoryLevelViewModel>
    {
        public DealerStartupInventoryLevelViewModelValidator()
        {
            RuleFor(f => f.DealerClassCode)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(f => f.PartId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(f => f.Quantity)
                .NotNull()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.IsActive)
                .NotNull()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity)
                .Must(CommonUtility.ValidateIntegerPartNullable(3))
                .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.Quantity)
                .Must(o => o.ToString().Length <= 6)
                .WithMessage(string.Format(MessageResource.Validation_Length, 6));
        }
    }
}
