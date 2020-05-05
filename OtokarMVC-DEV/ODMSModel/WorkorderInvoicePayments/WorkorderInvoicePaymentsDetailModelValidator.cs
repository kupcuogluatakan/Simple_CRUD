using FluentValidation;

namespace ODMSModel.WorkorderInvoicePayments
{
    public class WorkorderInvoicePaymentsDetailModelValidator : AbstractValidator<WorkorderInvoicePaymentsDetailModel>
    {
        public WorkorderInvoicePaymentsDetailModelValidator()
        {
            RuleFor(x => x.PayAmount)
                .GreaterThan(0)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(x => x.BankId)
                .NotNull()
                .When(x => x.BankRequired)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(x => x.InstalmentNumber)
                .NotNull()
                .When(x => x.InstalmentNumberRequired)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(x => x.TransmitNumber)
                .NotNull()
                .When(x => x.TransmitNumberRequired)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
