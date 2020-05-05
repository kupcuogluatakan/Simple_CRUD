using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.LabourSubGroup
{
    public class LabourSubGroupViewModelValidator : AbstractValidator<LabourSubGroupViewModel>
    {
        public LabourSubGroupViewModelValidator()
        {           
            RuleFor(c => c.SubGroupId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MainGroupId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SubGroupId).Length(0, 5).WithLocalizedMessage(() => MessageResource.Validation_Length, 5);
            RuleFor(c => c.Description).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);           
            RuleFor(c => c.MultiLanguageContentAsText).Must(CommonUtility.IsTurkishContentFilled).WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
