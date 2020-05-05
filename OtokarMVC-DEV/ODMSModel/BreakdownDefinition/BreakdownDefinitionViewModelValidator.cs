using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.BreakdownDefinition
{
    public class BreakdownDefinitionViewModelValidator : AbstractValidator<BreakdownDefinitionViewModel>
    {
        public BreakdownDefinitionViewModelValidator()
        {
            RuleFor(m => m.AdminDesc).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc)
            .Length(0, 100)
           .WithMessage(string.Format(MessageResource.Validation_Length, 100));

            RuleFor(c => c.PdiBreakdownCode).Length(0, 3).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.PdiBreakdownCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

            //RuleFor(m => m.BreakdownName)
            //   .NotEmpty()
            //   .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
