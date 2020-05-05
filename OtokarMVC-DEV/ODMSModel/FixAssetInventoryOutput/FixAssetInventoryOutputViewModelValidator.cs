using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.FixAssetInventoryOutput
{
    public class FixAssetInventoryOutputViewModelValidator : AbstractValidator<FixAssetInventoryOutputViewModel>
    {
        public FixAssetInventoryOutputViewModelValidator()
        {
            RuleFor(c => c.FixAssetStatus).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.RestockReason).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.RestockReason)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);

            RuleFor(c => c.IdWarehouse).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required).When(c=>c.FixAssetStatus == 2);
            RuleFor(c => c.IdRack).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required).When(c => c.FixAssetStatus == 2);
        }
    }
}
