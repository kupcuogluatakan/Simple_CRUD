using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartSaleDetail
{
    public class SparePartSaleDetailDetailModelValidator : AbstractValidator<SparePartSaleDetailDetailModel>
    {
        public SparePartSaleDetailDetailModelValidator()
        {
            RuleFor(x => x.SparePartId).NotNull().GreaterThan(0).WithName(MessageResource.SparePart_Display_PartName).WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.StatusId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required); 
            
            RuleFor(x => x.PartSaleId).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.PlanQuantity).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PlanQuantity).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PlanQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.PlanQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.PoQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.PoQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.PickQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.PickQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.DiscountRatio).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.DiscountRatio).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.DiscountRatio).LessThanOrEqualTo(100).GreaterThanOrEqualTo(0).WithLocalizedMessage(() => MessageResource.Validation_Invalid_DiscountRatio);

            RuleFor(c => c.DiscountPrice).Must(CommonUtility.ValidateIntegerPartNullable(6)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 6));
            RuleFor(c => c.DiscountPrice).Must(o => o.ToString().Length <= 10).WithMessage(string.Format(MessageResource.Validation_Length, 10));
        }
    }
}
