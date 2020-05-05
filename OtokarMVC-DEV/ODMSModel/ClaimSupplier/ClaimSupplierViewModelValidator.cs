using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.ClaimSupplier
{
    public class ClaimSupplierViewModelValidator : AbstractValidator<ClaimSupplierViewModel>
    {
        public ClaimSupplierViewModelValidator()
        {
            RuleFor(c => c.SupplierCode)
                .Length(0, 20)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.SupplierCode).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            
            RuleFor(c => c.SupplierName)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.SupplierName).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.ClaimRackCode)
                .Length(0, 20)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
        }
    }
}
