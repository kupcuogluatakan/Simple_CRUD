using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.Delivery
{
    public class DeliveryViewModelValidator : AbstractValidator<DeliveryCreateModel>
    {
        public DeliveryViewModelValidator()
        {
            RuleFor(c => c.SupplierId).NotEmpty().When(c => c.CommandType == "I").WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.WayBillNo).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.WayBillDate).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.PurchaseNo)
                .NotEmpty()
                .When(c => c.CommandType == "I")
                .WithLocalizedMessage(() => MessageResource.Validation_PurchaseOrderDeatilNotSelected);
            RuleFor(c => c.PurchaseNo)
                .Must(c => c.Length > 0)
                .When(c => c.CommandType == "I")
                .WithLocalizedMessage(() => MessageResource.Validation_PurchaseOrderDeatilNotSelected);
            RuleFor(c => c.InvoiceSerialNo).Length(0, 10).WithLocalizedMessage(() => MessageResource.Validation_Length, 10);
            RuleFor(c => c.InvoiceNo).Length(0, 15).WithLocalizedMessage(() => MessageResource.Validation_Length, 15);          
        }
    }
}
