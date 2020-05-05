using FluentValidation;

namespace ODMSModel.VehicleModel
{
    public class VehicleModelIndexViewModelValidator : AbstractValidator<VehicleModelIndexViewModel>
    {
        public VehicleModelIndexViewModelValidator()
        {
            //RuleFor(m => m.VehicleModelSSID)
            //    .NotEmpty()
            //    .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            //RuleFor(c => c.VehicleModelSSID)
            //    .Length(0, 30)
            //    .WithMessage(string.Format(MessageResource.Validation_Length, 30));
        }
    }
}
