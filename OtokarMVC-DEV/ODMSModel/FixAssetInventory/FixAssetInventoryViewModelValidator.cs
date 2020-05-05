using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.FixAssetInventory
{
    public class FixAssetInventoryViewModelValidator : AbstractValidator<FixAssetInventoryViewModel>
    {
        public FixAssetInventoryViewModelValidator()
        {
            RuleFor(c => c.EquipmentTypeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Code).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Name).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Description).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StatusId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartId).NotNull().When(c => c.IsPartOriginal.GetValue<bool>()).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.RackId).NotNull().When(c => c.IsPartOriginal.GetValue<bool>()).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Code)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Name)
                .Length(0, 200)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.SerialNo)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Description)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Unit)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.RestockReason)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
        }
    }
}
