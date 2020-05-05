using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.PDIControlPartDefinition
{
    public class PDIControlPartDefinitionViewModelValidator : AbstractValidator<PDIControlPartDefinitionViewModel>
    {
        public PDIControlPartDefinitionViewModelValidator()
        {
            RuleFor(m => m.IdPDIControlDefinition)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.PDIPartCode)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.IsActive)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
