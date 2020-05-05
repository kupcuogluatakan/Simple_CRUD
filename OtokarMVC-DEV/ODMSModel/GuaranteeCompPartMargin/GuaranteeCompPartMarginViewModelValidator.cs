using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.GuaranteeCompPartMargin
{
    public class GuaranteeCompPartMarginViewModelValidator : AbstractValidator<GuaranteeCompPartMarginViewModel>
    {
        public GuaranteeCompPartMarginViewModelValidator()
        {
            RuleFor(c => c.CountryId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.MaxPrice).GreaterThan(0).WithLocalizedMessage(() => MessageResource.Validation_GreaterThanZero);
            RuleFor(c => c.MaxPrice).Must(o => o.ToString().Length <= 9).WithMessage(string.Format(MessageResource.Validation_Length, 9));

            RuleFor(c => c.GrntPrice).NotNull().When(c => c.GrntRatio == null).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.GrntPrice).GreaterThan(0).When(c => c.GrntRatio == null).WithLocalizedMessage(() => MessageResource.Validation_GreaterThanZero);
            RuleFor(c => c.GrntPrice).Must(CommonUtility.ValidateIntegerPartNullable(3)).When(c => c.GrntRatio == null).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.GrntPrice).Must(o => o.ToString().Length <= 6).When(c => c.GrntRatio == null).WithMessage(string.Format(MessageResource.Validation_Length, 6));

            RuleFor(c => c.GrntRatio).NotNull().When(c => c.GrntPrice == null).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.GrntRatio).GreaterThan(0).When(c => c.GrntPrice == null).WithLocalizedMessage(() => MessageResource.Validation_GreaterThanZero);
            RuleFor(c => c.GrntRatio).Must(CommonUtility.ValidateIntegerPartNullable(3)).When(c => c.GrntPrice == null).WithMessage(string.Format(MessageResource.Validation_IntegerLength, 3));
            RuleFor(c => c.GrntRatio).Must(o => o.ToString().Length <= 6).When(c => c.GrntPrice == null).WithMessage(string.Format(MessageResource.Validation_Length, 6));
        }
    }
}
