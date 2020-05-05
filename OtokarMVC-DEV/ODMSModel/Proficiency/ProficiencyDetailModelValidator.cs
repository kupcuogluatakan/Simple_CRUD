using FluentValidation;
using ODMSCommon;

namespace ODMSModel.Proficiency
{
    public class ProficiencyDetailModelValidator : AbstractValidator<ProficiencyDetailModel>
    {
        public ProficiencyDetailModelValidator()
        {
            RuleFor(c => c.ProficiencyCode)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

        }
    }
}
