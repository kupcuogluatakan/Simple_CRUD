using System.Globalization;
using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.ServiceCallSchedule
{
    public class ServiceCallScheduleViewModelValidator : AbstractValidator<ServiceCallScheduleViewModel>
    {
        public ServiceCallScheduleViewModelValidator()
        {
            RuleFor(c => c.CallIntervalMinute)
               .Must(o => o.ToString(CultureInfo.InvariantCulture).Length <= 5)
               .WithLocalizedMessage(() => MessageResource.Validation_Length, 5);

            RuleFor(c => c.ServiceDescription)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.CallIntervalMinute)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
