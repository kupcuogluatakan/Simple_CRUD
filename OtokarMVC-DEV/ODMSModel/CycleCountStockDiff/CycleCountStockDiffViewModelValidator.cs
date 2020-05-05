using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CycleCountStockDiff
{
    public class CycleCountStockDiffViewModelValidator : AbstractValidator<CycleCountStockDiffViewModel>
    {
        public CycleCountStockDiffViewModelValidator()
        {
            RuleFor(c => c.WarehouseId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CycleCountId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StockCardId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AfterCount).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
