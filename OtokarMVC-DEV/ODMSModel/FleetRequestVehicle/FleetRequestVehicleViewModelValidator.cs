using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.FleetRequestVehicle
{
    public class FleetRequestVehicleViewModelValidator : AbstractValidator<FleetRequestVehicleViewModel>
    {
        public FleetRequestVehicleViewModelValidator()
        {
            RuleFor(c => c.FleetRequestId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.VehicleId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage(MessageResource.Validation_Required);
        }
    }
}
