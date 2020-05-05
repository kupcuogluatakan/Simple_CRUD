using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.PeriodicMaintControlList
{
    public class PeriodicMaintControlListViewModelValidator : AbstractValidator<PeriodicMaintControlListViewModel>
    {
        public PeriodicMaintControlListViewModelValidator()
        {
            RuleFor(c => c.IdType).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            //RuleFor(c => c.DocId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DocumentDesc).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.LanguageCustom).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.EngineType).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
