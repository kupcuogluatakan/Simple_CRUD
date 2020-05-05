using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.DealerVehicleGroup
{
    public class DealerVehicleGroupViewModelValidator : AbstractValidator<DealerVehicleGroupViewModel>
    {
        public DealerVehicleGroupViewModelValidator()
        {
            RuleFor(c => c.DealerId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.VehicleGroupId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.VehicleModelCode).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
