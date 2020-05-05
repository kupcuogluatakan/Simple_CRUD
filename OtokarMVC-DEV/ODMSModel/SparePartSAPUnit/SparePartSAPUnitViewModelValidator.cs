using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartSAPUnit
{
    public class SparePartSAPUnitViewModelValidator : AbstractValidator<SparePartSAPUnitViewModel>
    {
        public SparePartSAPUnitViewModelValidator()
        {
            RuleFor(v => v.UnitId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.ShipQuantity).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.PartId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
