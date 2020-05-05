using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.ClaimSupplierPart
{
    public class ClaimSupplierPartViewModelValidator : AbstractValidator<ClaimSupplierPartViewModel>
    {
        public ClaimSupplierPartViewModelValidator()
        {
            RuleFor(c => c.SupplierCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

        }
    }
}
