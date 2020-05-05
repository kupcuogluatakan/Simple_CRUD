using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.CriticalStockCard
{
    public class CriticalStockCardViewModelValidator : AbstractValidator<CriticalStockCardViewModel>
    {
        public CriticalStockCardViewModelValidator()
        {
            RuleFor(c => c.IdDealer).NotNull().WithLocalizedMessage(() => MessageResource.Validation_NotNull);
            RuleFor(c => c.IdPart).NotNull().WithLocalizedMessage(() => MessageResource.Validation_NotNull);
            RuleFor(c => c.CriticalStockQuantity).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CriticalStockQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.CriticalStockQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));
        }
    }
}
