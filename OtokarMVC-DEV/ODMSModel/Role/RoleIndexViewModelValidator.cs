using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.Role
{
    public class RoleIndexViewModelValidator : AbstractValidator<RoleIndexViewModel>
    {
        public RoleIndexViewModelValidator()
        {
            RuleFor(c => c.AdminDesc).NotEmpty().WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.MultiLanguageContentAsText).Must(CommonUtility.IsTurkishContentFilled).WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.MultiLanguageContentAsText).Length(0, 30).WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(v => v.AdminDesc).Length(0, 30).WithMessage(string.Format(MessageResource.Validation_Length, 30));
        }

    }
}

