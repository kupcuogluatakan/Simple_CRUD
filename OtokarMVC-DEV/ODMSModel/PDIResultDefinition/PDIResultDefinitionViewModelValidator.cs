using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.PDIResultDefinition
{
    public class PDIResultDefinitionViewModelValidator : AbstractValidator<PDIResultDefinitionViewModel>
    {
        public PDIResultDefinitionViewModelValidator()
        {
            RuleFor(c => c.PDIResultCode)
                .Length(0, 3)
               .WithMessage(string.Format(MessageResource.Validation_Length, 3));
            RuleFor(m => m.PDIResultCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.AdminDesc).Length(0, 100).WithMessage(string.Format(MessageResource.Validation_Length, 100));
            RuleFor(m => m.AdminDesc)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.IsActive)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.ResultNameML)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
