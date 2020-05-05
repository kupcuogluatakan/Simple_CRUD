using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.AppointmentIndicatorCategory
{
    public class AppointmentIndicatorCategoryViewModelValidator : AbstractValidator<AppointmentIndicatorCategoryViewModel>
    {
        public AppointmentIndicatorCategoryViewModelValidator()
        {
            RuleFor(m => m.AppointmentIndicatorMainCategoryId)
              .NotNull()
              .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.AdminDesc)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc)
                .Length(0, 150)
                .WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(m => m.Code)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.Code)
                .Length(0, 3)
                .WithMessage(string.Format(MessageResource.Validation_Length, 3));
            RuleFor(m => m.AppointmentIndicatorCategoryName)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
