using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.FleetVehicle
{
    public class FleetVehicleViewModelValidator : AbstractValidator<FleetVehicleViewModel>
    {
        public FleetVehicleViewModelValidator()
        {
            RuleFor(c => c.FleetId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.VehicleId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage(MessageResource.Validation_Required);
        }
    }
}
