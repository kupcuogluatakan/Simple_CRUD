using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.BodyworkDetail
{
    public class BodyworkDetailModelValidator : AbstractValidator<BodyworkDetailViewModel>
    {
        public BodyworkDetailModelValidator()
        {
            //BodyworkCode
            RuleFor(m => m.BodyworkCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.BodyworkCode)
                .Length(0, 20)
                .WithMessage(string.Format(MessageResource.Validation_Length, 20));
            //Desc
            RuleFor(m => m.Descripion)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Descripion)
                .Length(0, 100)
                .WithMessage(string.Format(MessageResource.Validation_Length, 100));
            //ML
            RuleFor(m => m.BodyworkDetailName)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
