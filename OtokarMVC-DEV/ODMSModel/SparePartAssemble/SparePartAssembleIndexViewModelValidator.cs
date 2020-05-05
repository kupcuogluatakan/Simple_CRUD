using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartAssemble
{
    public class SparePartAssembleIndexViewModelValidator : AbstractValidator<SparePartAssembleIndexViewModel>
    {
        public SparePartAssembleIndexViewModelValidator()
        {
            RuleFor(c => c.IdPart).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdPartAssemble).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_NotNull);
            RuleFor(c => c.Quantity).NotNull().WithLocalizedMessage(() => MessageResource.Validation_NotNull);

            RuleFor(c => c.Quantity).LessThan(1000).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity).GreaterThanOrEqualTo(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
