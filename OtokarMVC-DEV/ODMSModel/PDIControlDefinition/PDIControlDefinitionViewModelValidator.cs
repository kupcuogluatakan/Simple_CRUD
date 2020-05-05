using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.PDIControlDefinition
{
    public class PDIControlDefinitionViewModelValidator : AbstractValidator<PDIControlDefinitionViewModel>
    {
        public PDIControlDefinitionViewModelValidator()
        {
            RuleFor(c => c.PDIControlCode)
                .Length(0, 5)
               .WithMessage(string.Format(MessageResource.Validation_Length, 5));
            RuleFor(m => m.PDIControlCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(c => c.ModelKod).Length(0, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20));
            RuleFor(m => m.ModelKod)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.RowNo)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.IsActive)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.IsGroupCode)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.ControlNameML)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
