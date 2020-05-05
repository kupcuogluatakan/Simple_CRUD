using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.GuaranteeAuthorityGroup
{
    public class GuaranteeAuthorityGroupViewModelValidator : AbstractValidator<GuaranteeAuthorityGroupViewModel>
    {
        public GuaranteeAuthorityGroupViewModelValidator()
        {
            RuleFor(c => c.GroupName).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.GroupName).Length(0,100).WithLocalizedMessage(()=>MessageResource.Validation_Length,100);
            RuleFor(c => c.MailList).Length(0, 500).WithLocalizedMessage(() => MessageResource.Validation_Length, 500);
        }
    }
}
