using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartSaleWaybill
{
    public class SparePartSaleWaybillViewModelValidator : AbstractValidator<SparePartSaleWaybillViewModel>
    {
        public SparePartSaleWaybillViewModelValidator()
        {
            RuleFor(f => f.WaybillNo)
                .NotEmpty().When(e => !e.IsWaybilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.WaybillSerialNo)
                .NotEmpty().When(e => !e.IsWaybilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.WaybillDate)
                .NotEmpty().When(e => !e.IsWaybilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.ShippingAddressId)
                .NotEmpty().When(e => !e.IsWaybilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.CustomerId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.InvoiceNo)
                .NotEmpty().When(e => e.IsWaybilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.InvoiceSerialNo)
                .NotEmpty().When(e => e.IsWaybilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.InvoiceDate)
                .NotEmpty().When(e => e.IsWaybilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.BillingAddressId)
                .NotEmpty().When(e => e.IsWaybilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.InstalmentNumber)
                .NotEmpty().When(e => e.InstalmentRequired)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.BankId)
                .NotEmpty().When(e => e.BankRequired)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(f => f.DueDuration)
                .NotEmpty().When(e => e.DefermentRequired)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

        }
    }
}
