using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.MaintenanceLabour
{
    public class MaintenanceLabourViewModelValidator : AbstractValidator<MaintenanceLabourViewModel>
    {
        public MaintenanceLabourViewModelValidator()
        {
            RuleFor(c => c.MaintenanceId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MaintenanceId).NotEqual(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.LabourId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.LabourId).NotEqual(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
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
