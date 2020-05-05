using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.Vehicle
{
    public class VehicleContactInfoModelValidator : AbstractValidator<VehicleContactInfoModel>
    {
        public VehicleContactInfoModelValidator()
        {
            RuleFor(c => c.Location).Length(0, 30).WithLocalizedMessage(() => MessageResource.Validation_Length, 30);
            RuleFor(c => c.ResponsiblePerson).Length(0, 30).WithLocalizedMessage(() => MessageResource.Validation_Length, 30);
            RuleFor(c => c.ResponsiblePersonPhone).Length(0, 30).WithLocalizedMessage(() => MessageResource.Validation_Length, 20);
        }
    }
}
