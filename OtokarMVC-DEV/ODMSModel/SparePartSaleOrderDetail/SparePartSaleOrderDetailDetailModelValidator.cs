using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartSaleOrderDetail
{
    public class SparePartSaleOrderDetailDetailModelValidator : AbstractValidator<SparePartSaleOrderDetailDetailModel>
    {
        public SparePartSaleOrderDetailDetailModelValidator()
        {
            RuleFor(x => x.SoNumber).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(x => x.SparePartId).NotNull().GreaterThan(0).WithName(MessageResource.SparePart_Display_PartName).WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.PlannedQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.PlannedQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.ShippedQuantity).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.ShippedQuantity).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.ListPrice).Must(CommonUtility.ValidateIntegerPartNullable(6)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.ListPrice).Must(o => o.ToString().Length <= 10).WithMessage(string.Format(MessageResource.Validation_Length, 10));

            RuleFor(c => c.OrderPrice).Must(CommonUtility.ValidateIntegerPartNullable(6)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.OrderPrice).Must(o => o.ToString().Length <= 10).WithMessage(string.Format(MessageResource.Validation_Length, 10));

            RuleFor(c => c.ConfirmPrice).Must(CommonUtility.ValidateIntegerPartNullable(6)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.ConfirmPrice).Must(o => o.ToString().Length <= 10).WithMessage(string.Format(MessageResource.Validation_Length, 10));

            RuleFor(c => c.ListDiscountRatio).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.ListDiscountRatio).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.AppliedDiscountRatio).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.ListDiscountRatio).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.StatusId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
