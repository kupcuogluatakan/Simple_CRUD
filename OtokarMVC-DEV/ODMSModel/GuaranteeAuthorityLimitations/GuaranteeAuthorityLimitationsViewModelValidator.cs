using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.GuaranteeAuthorityLimitations
{
    public class GuaranteeAuthorityLimitationsViewModelValidator : AbstractValidator<GuaranteeAuthorityLimitationsViewModel>
    {
        public GuaranteeAuthorityLimitationsViewModelValidator()
        {
            RuleFor(c => c.Amount).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CumulativeAmount).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CurrencyCode).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.ModelKod).NotEmpty().WithMessage(MessageResource.Validation_Required);

            RuleFor(c => c.Amount)
                .Must(CommonUtility.ValidateIntegerPart(9))
                .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 9));
            RuleFor(c => c.Amount)
                .Must(o => o.ToString().Length <= 12)
                .WithMessage(string.Format(MessageResource.Validation_Length, 12));

            RuleFor(c => c.CumulativeAmount)
                .Must(CommonUtility.ValidateIntegerPart(10))
                .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 10));
            RuleFor(c => c.CumulativeAmount)
                .Must(o => o.ToString().Length <= 13)
                .WithMessage(string.Format(MessageResource.Validation_Length, 13));


        }
    }
}
