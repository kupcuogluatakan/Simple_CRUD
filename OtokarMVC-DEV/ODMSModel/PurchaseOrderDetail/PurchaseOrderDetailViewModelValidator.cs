using System;
using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.PurchaseOrderDetail
{
    public class PurchaseOrderDetailViewModelValidator : AbstractValidator<PurchaseOrderDetailViewModel>
    {
        public PurchaseOrderDetailViewModelValidator()
        {
            RuleFor(c => c.PurchaseOrderNumber).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StatusId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.PackageQuantity).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PackageQuantity).Must(CommonUtility.ValidateIntegerPartNullable(5)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 5));
            RuleFor(c => c.PackageQuantity).Must(o => o.ToString().Length <= 8).WithMessage(string.Format(MessageResource.Validation_Length, 8));

            RuleFor(c => c.DesireQuantity).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DesireQuantity).Must(CommonUtility.ValidateIntegerPartNullable(5)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 5));
            RuleFor(c => c.DesireQuantity).Must(o => o.ToString().Length <= 8).WithMessage(string.Format(MessageResource.Validation_Length, 8));

            RuleFor(c => c.OrderQuantity).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.OrderQuantity).Must(CommonUtility.ValidateIntegerPartNullable(5)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 5));
            RuleFor(c => c.OrderQuantity).Must(o => o.ToString().Length <= 8).WithMessage(string.Format(MessageResource.Validation_Length, 8));

            RuleFor(c => c.OrderPrice).GreaterThan(0).When(x=>x.ManuelPriceAllow == true).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.OrderPrice).Must(CommonUtility.ValidateIntegerPartNullable(7)).When(x => x.ManuelPriceAllow == true).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 7));
            RuleFor(c => c.OrderPrice).Must(o => o.ToString().Length <= 10).When(x => x.ManuelPriceAllow == true).WithMessage(string.Format(MessageResource.Validation_Length, 10));
            RuleFor(c => c.OrderPrice).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.DesireDeliveryDate)
                .GreaterThanOrEqualTo(DateTime.Today).When(x=>x.PurchaseOrderDetailSeqNo==0)
                .WithLocalizedMessage(() => MessageResource.Validation_GreaterThanToday);
        }
    }
}
