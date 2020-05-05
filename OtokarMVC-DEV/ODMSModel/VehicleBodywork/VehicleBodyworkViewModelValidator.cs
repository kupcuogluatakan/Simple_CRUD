using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.VehicleBodywork
{
    public class VehicleBodyworkViewModelValidator : AbstractValidator<VehicleBodyworkViewModel>
    {
        public VehicleBodyworkViewModelValidator()
        {
            RuleFor(q => q.BodyworkCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.VehicleId)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.CityId)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.CountryId)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.Manufacturer)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.Manufacturer)
                .Length(0, 50)
                .WithMessage(string.Format(MessageResource.Validation_Length, 50));
        }
    }
}
