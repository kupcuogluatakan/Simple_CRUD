using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.PDIPartDefinition
{
    public class PDIPartDefinitionViewModelValidator : AbstractValidator<PDIPartDefinitionViewModel>
    {
        public PDIPartDefinitionViewModelValidator()
        {
            RuleFor(m => m.AdminDesc).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc)
            .Length(0, 100)
           .WithMessage(string.Format(MessageResource.Validation_Length, 100));

            RuleFor(c => c.PdiPartCode).Length(0, 50).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.PdiPartCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

            //RuleFor(m => m.MultiLanguageContentAsText).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
