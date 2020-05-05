using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.AppointmentIndicatorMainCategory
{
    public class AppointmentIndicatorMainCategoryViewModelValidator : AbstractValidator<AppointmentIndicatorMainCategoryViewModel>
    {
        public AppointmentIndicatorMainCategoryViewModelValidator()
        {
            RuleFor(m => m.AdminDesc)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc)
                .Length(0, 150)
                .WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(m => m.MainCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.MainCode)
                .Length(0, 3)
                .WithMessage(string.Format(MessageResource.Validation_Length, 3));
            RuleFor(m => m.AppointmentIndicatorMainCategoryName)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
