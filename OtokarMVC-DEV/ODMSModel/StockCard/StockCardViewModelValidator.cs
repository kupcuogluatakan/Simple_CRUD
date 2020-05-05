using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.StockCard
{
    public class StockCardViewModelValidator : AbstractValidator<StockCardViewModel>
    {
        public StockCardViewModelValidator()
        {
            RuleFor(c => c.PartId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DealerId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.AutoOrder).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CalculatedPrice).Equal(true).WithLocalizedMessage(() => MessageResource.StockCard_Warning_SalesPriceMustBeGreaterThanCalculatedPrice);

            RuleFor(c => c.MinStockQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.MinStockQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.MaxStockQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.MaxStockQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.MinSaleQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.MinSaleQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.ProfitMarginRatio).NotNull().When(m => m.IsOriginalPart == true).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ProfitMarginRatio).GreaterThan(0).When(m => m.IsOriginalPart == true).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ProfitMarginRatio).Must(CommonUtility.ValidateIntegerPartNullable(4)).When(m => m.IsOriginalPart == true).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 4));
            RuleFor(c => c.ProfitMarginRatio).Must(o => o.ToString().Length <= 7).When(m => m.IsOriginalPart == true).WithMessage(string.Format(MessageResource.Validation_Length, 7));

            RuleFor(c => c.SalePrice).NotNull().When(c => c.IsOriginalPart == false).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SalePrice).GreaterThan(0).When(c => c.IsOriginalPart == false).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SalePrice).Must(CommonUtility.ValidateIntegerPartNullable(7)).When(c => c.IsOriginalPart == false).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 7));
            RuleFor(c => c.SalePrice).Must(o => o.ToString().Length <= 10).When(c => c.IsOriginalPart == false).WithMessage(string.Format(MessageResource.Validation_Length, 10));

            RuleFor(c => c.WarehouseId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
          

            RuleFor(c => c.RackId)
                .NotNull()
                .When(c => c.WarehouseId != null)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }


    }
}

