using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.Title
{
    public class TitleIndexModelValidator : AbstractValidator<TitleIndexViewModel>
    {
        public TitleIndexModelValidator()
        {
            RuleFor(c => c.TitleName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MultiLanguageContentAsText).Must(CommonUtility.IsTurkishContentFilled).WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
