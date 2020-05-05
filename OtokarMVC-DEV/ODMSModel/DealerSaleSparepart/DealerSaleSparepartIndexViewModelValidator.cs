using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.DealerSaleSparepart
{
    public class DealerSaleSparepartIndexViewModelValidator : AbstractValidator<DealerSaleSparepartIndexViewModel>
    {
        public DealerSaleSparepartIndexViewModelValidator()
        {
            //RuleFor(c => c.DiscountRatio).LessThanOrEqualTo(100).GreaterThanOrEqualTo(0).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);
            RuleFor(c => c.IdPart).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DiscountRatio).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DiscountRatio).LessThanOrEqualTo((decimal)99.99).WithMessage(string.Format(MessageResource.Validation_IntegerLength,99.99));//.WithLocalizedMessage(() => MessageResource.Validation_MaxValue,99.99);
            RuleFor(c => c.DiscountRatio).GreaterThanOrEqualTo(0).WithLocalizedMessage(() => MessageResource.Validation_GreaterThanZero);//,(c => c.DiscountRatio));
            //RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
