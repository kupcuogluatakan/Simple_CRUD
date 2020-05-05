using System;
using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrderInvoice
{
    public class WorkOrderInvoicesViewModelValidator : AbstractValidator<WorkOrderInvoicesViewModel>
    {
        public WorkOrderInvoicesViewModelValidator()
        {
            //required
            RuleFor(c => c.WorkOrderId).NotEmpty().When(c => string.IsNullOrEmpty(c.WorkOrderIds)).WithLocalizedName(() => MessageResource.Validation_Required);
            RuleFor(c => c.CustomerId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AddressId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.InvoiceSerialNo).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.InvoiceNo).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.InvoiceDate).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.WorkOrderIds).NotEmpty().When(c => c.WorkOrderInvoiceId == 0).WithLocalizedName(() => MessageResource.Validation_Required);
            //lenght
            RuleFor(c => c.InvoiceSerialNo).Length(0, 10).WithLocalizedMessage(() => MessageResource.Validation_Length, 10);
            RuleFor(c => c.InvoiceNo).Length(0, 15).WithLocalizedMessage(() => MessageResource.Validation_Length, 15);
            RuleFor(c => c.InvoiceRatio)
                .Must((m, p) => p.GetValueOrDefault(0).ToString().Length <= 6)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 6);
            //other
            RuleFor(c => c.InvoiceDate)
                .Must(p => p.Date > DateTime.Now.AddDays(-1))
                .When(c => c.WorkOrderInvoiceId == 0)
                .WithLocalizedMessage(() => MessageResource.Validation_InvoiceDateLessThanToday);
            RuleFor(c => c.InvoiceTypeId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.SpecialInvoiceAmount)
               .NotEmpty()
               .When(c => c.InvoiceTypeId == 3)
               .WithMessage(MessageResource.Validation_Required);

            RuleFor(c => c.SpecialInvoiceAmount)
               .Must(CommonUtility.ValidateIntegerPart(6))
               .When(c => c.InvoiceTypeId == 3)
               .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 6));
            RuleFor(c => c.SpecialInvoiceAmount)
                .Must(o => o.ToString().Length <= 10)
                .When(c => c.InvoiceTypeId == 3)
                .WithMessage(string.Format(MessageResource.Validation_Length, 9));

            RuleFor(c => c.SpecialInvoiceVatAmount)
               .NotEmpty()
               .When(c => c.InvoiceTypeId == 3)
               .WithMessage(MessageResource.Validation_Required);

            RuleFor(c => c.SpecialInvoiceVatAmount)
               .Must(CommonUtility.ValidateIntegerPart(6))
               .When(c => c.InvoiceTypeId == 3)
               .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 6));
            RuleFor(c => c.SpecialInvoiceVatAmount)
                .Must(o => o.ToString().Length <= 10)
                .When(c => c.InvoiceTypeId == 3)
                .WithMessage(string.Format(MessageResource.Validation_Length, 9));

            RuleFor(c => c.SpecialInvoiceDescription)
                .NotEmpty()
                .When(c => c.InvoiceTypeId == 3)
                .WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.SpecialInvoiceDescription)
                .Length(0, 100)
                .When(c => c.InvoiceTypeId == 3)
                .WithLocalizedMessage(() => MessageResource.Validation_Length, 100);
        }
    }
}
