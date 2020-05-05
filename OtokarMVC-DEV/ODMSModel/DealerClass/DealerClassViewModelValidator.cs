using FluentValidation;
using ODMSCommon.Resources;
using System.Text.RegularExpressions;

namespace ODMSModel.DealerClass
{
    public class DealerClassViewModelValidator : AbstractValidator<DealerClassViewModel>
    {
        public DealerClassViewModelValidator()
        {
            Regex antiCharRgx = new Regex("^[a-zA-Z0-9]+$");

            RuleFor(c => c.DealerClassCode).NotNull().Length(1, 2).WithMessage(string.Format(MessageResource.Validation_Length, 2));
            RuleFor(c => c.DealerClassCode).Matches(antiCharRgx).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SSIdDealerClass).NotNull().Length(1, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20));
            RuleFor(c => c.DealerClassName).NotEmpty().Length(1, 100).WithMessage(string.Format(MessageResource.Validation_Length, 100));
            RuleFor(c => c.IsActive).NotNull().WithMessage(string.Format(MessageResource.Validation_Required));//.WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
