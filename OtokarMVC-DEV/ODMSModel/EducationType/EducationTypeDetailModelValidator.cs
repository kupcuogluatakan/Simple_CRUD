using FluentValidation;
using ODMSCommon;

namespace ODMSModel.EducationType
{
    public class EducationTypeDetailModelValidator : AbstractValidator<EducationTypeDetailModel>
    {
        public EducationTypeDetailModelValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
        }
    }
}
