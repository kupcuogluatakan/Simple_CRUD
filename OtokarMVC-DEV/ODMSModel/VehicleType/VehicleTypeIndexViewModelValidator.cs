using FluentValidation;

namespace ODMSModel.VehicleType
{
    public class VehicleTypeIndexViewModelValidator : AbstractValidator<VehicleTypeIndexViewModel>
    {
        public VehicleTypeIndexViewModelValidator()
        {
            //type SSID is removed 
            //keeping the class for future needs. Taner

            //RuleFor(m => m.TypeSSID)
            //   .NotEmpty()
            //   .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            //RuleFor(c => c.TypeSSID)
            //    .Length(0, 30)
            //    .WithMessage(string.Format(MessageResource.Validation_Length, 30));
        }
    }
}
