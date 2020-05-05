using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CycleCountResult
{
    public class CycleCountResultViewModelValidator : AbstractValidator<CycleCountResultViewModel>
    {
        public CycleCountResultViewModelValidator()
        {
            RuleFor(c => c.WarehouseId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.RackId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StockCardId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CycleCountId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
