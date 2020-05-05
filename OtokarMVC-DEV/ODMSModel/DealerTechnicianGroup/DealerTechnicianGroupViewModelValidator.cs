using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.DealerTechnicianGroup
{
    public class DealerTechnicianGroupViewModelValidator : AbstractValidator<DealerTechnicianGroupViewModel>
    {
        public DealerTechnicianGroupViewModelValidator()
        {
            RuleFor(c => c.TechnicianGroupName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.TechnicianGroupName)
               .Length(0, 300)
               .WithLocalizedMessage(() => MessageResource.Validation_Length, 300); 
            RuleFor(c => c.Description)
                 .Length(0, 100)
                 .WithLocalizedMessage(() => MessageResource.Validation_Length, 100);
            RuleFor(c => c.VehicleModelKodList).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
