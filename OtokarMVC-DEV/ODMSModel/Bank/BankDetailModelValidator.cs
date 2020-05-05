using FluentValidation;

namespace ODMSModel.Bank
{
    public class BankDetailModelValidator : AbstractValidator<BankDetailModel>
    {
        public BankDetailModelValidator()
        {
            RuleFor(f => f.Code)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(f => f.Name)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
