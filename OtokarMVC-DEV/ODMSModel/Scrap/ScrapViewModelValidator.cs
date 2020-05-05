using FluentValidation;

namespace ODMSModel.Scrap
{
    public class ScrapViewModelValidator : AbstractValidator<ScrapViewModel>
    {
        public ScrapViewModelValidator()
        {
            RuleFor(c => c.ScrapDate)
                .NotEmpty()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.ScrapId)
                .NotNull()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            
        }
    }
}
