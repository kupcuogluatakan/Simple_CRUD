using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.AppointmentDetailsLabours
{
    public class AppointmentDetailsLaboursViewModelValidator : AbstractValidator<AppointmentDetailsLaboursViewModel>
    {
        public AppointmentDetailsLaboursViewModelValidator()
        {
            RuleFor(c => c.AppointmentIndicatorId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.LabourId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity)
                .Must((m, p) => p.ToString().Length <= 2)
                .WithMessage(string.Format(MessageResource.Validation_Length, 2));
            //RuleFor(c => c.Duration)
            //    .NotEmpty()
            //    .WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.Duration)
            //    .Must((m, p) => p.ToString().Length <= 4)
            //    .WithMessage(string.Format(MessageResource.Validation_Length, 4));

        }
    }
}
