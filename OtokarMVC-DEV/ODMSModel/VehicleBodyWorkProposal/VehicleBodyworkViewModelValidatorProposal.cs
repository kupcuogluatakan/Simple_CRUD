using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.VehicleBodyWorkProposal
{
    public class VehicleBodyworkViewModelValidatorProposal : AbstractValidator<VehicleBodyworkViewModelProposal>
    {
        public VehicleBodyworkViewModelValidatorProposal()
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
