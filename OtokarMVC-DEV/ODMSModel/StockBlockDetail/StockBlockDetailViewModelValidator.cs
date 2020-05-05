using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.StockBlockDetail
{
    public class StockBlockDetailViewModelValidator : AbstractValidator<StockBlockDetailViewModel>
    {
        public StockBlockDetailViewModelValidator()
        {
            RuleFor(c => c.IdPart).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.BlockQty).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.BlockQty).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_GreaterThanZero, MessageResource.StockBlock_Display_BlockQty);
            RuleFor(c => c.IdStockType).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
