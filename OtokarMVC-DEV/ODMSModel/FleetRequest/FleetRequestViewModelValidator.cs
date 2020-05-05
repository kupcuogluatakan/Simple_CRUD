using FluentValidation;

namespace ODMSModel.FleetRequest
{
    public class FleetRequestViewModelValidator : AbstractValidator<FleetRequestViewModel>
    {
        public FleetRequestViewModelValidator()
        {
            RuleFor(c => c.StatusId)
                .NotNull()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(x => x.Description)
              .NotEmpty()
              .Length(1, 300)
              .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
