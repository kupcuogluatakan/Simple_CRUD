using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.GuaranteeCompLabourMargin
{
    public class GuaranteeCompLabourMarginViewModelValidator : AbstractValidator<GuaranteeCompLabourMarginViewModel>
    {
        public GuaranteeCompLabourMarginViewModelValidator()
        {
            RuleFor(c => c.CountryId).NotEqual(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CountryId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.MaxPrice).Must(o => o.ToString().Length <= 4).WithMessage(string.Format(MessageResource.Validation_Length, 4));

            RuleFor(c => c.GrntPrice).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.GrntPrice).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.GrntRatio).Must(CommonUtility.ValidateIntegerPartNullable(3)).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.GrntRatio).Must(o => o.ToString().Length <= 6).WithMessage(string.Format(MessageResource.Validation_Length, 6));
        }
    }
}
