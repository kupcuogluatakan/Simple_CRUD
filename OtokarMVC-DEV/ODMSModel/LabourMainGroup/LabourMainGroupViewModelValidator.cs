using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.LabourMainGroup
{
    public class LabourMainGroupViewModelValidator : AbstractValidator<LabourMainGroupViewModel>
    {
        public LabourMainGroupViewModelValidator()
        {
            RuleFor(c => c.MainGroupId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MainGroupId).Length(0, 4).WithLocalizedMessage(() => MessageResource.Validation_Length, 4);
            RuleFor(c => c.Description).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Description).Length(0, 100).WithLocalizedMessage(() => MessageResource.Validation_Length, 100);
            RuleFor(c => c.MultiLanguageContentAsText).Must(CommonUtility.IsTurkishContentFilled).WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
