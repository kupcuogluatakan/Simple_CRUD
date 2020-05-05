using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.StockRackDetail
{
    public class StockExchangeViewModelValidator : AbstractValidator<StockExchangeViewModel>
    {
        public StockExchangeViewModelValidator()
        {
            RuleFor(c => c.PartId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.FromRackId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ToRackId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.StockTypeId).NotNull().When(e => e.FromWarehouseId != e.ToWarehouseId).WithLocalizedMessage(() => MessageResource.StockExchange_Warning_StockTypeEmpty);

            RuleFor(c => c.Description).Length(0, 100).WithLocalizedMessage(() => MessageResource.StockExhange_Warning_DescriptionLength);

            RuleFor(c => c.Quantity).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity).GreaterThan(0).WithLocalizedMessage(() => MessageResource.StockExchange_Warning_QuantityLessThenZero);
            RuleFor(c => c.Quantity).LessThanOrEqualTo(e => e.MaxQuantity).WithLocalizedMessage(() => MessageResource.StockExchange_Warning_QuantityLessThenMaxQuantity);
        }
    }
}

