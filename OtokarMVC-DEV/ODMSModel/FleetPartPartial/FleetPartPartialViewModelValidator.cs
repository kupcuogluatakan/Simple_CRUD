using FluentValidation;

namespace ODMSModel.FleetPartPartial
{
    public class FleetPartPartialViewModelValidator : AbstractValidator<FleetPartViewModel>
    {
        public FleetPartPartialViewModelValidator()
        {
            //RuleFor(c => c.PartName).NotEmpty().WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
