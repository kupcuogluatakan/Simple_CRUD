using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.AppointmentIndicatorSubCategory
{
    public class AppointmentIndicatorSubCategoryViewModelValidator : AbstractValidator<AppointmentIndicatorSubCategoryViewModel>
    {
        public AppointmentIndicatorSubCategoryViewModelValidator()
        {
            RuleFor(m => m.IsAutoCreate)
                 .NotNull()
                 .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.AppointmentIndicatorCategoryId)
              .NotNull()
              .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.AdminDesc)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc)
                .Length(0, 150)
                .WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(m => m.SubCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.SubCode)
                .Length(0, 9)
                .WithMessage(string.Format(MessageResource.Validation_Length, 9));
            RuleFor(m => m.AppointmentIndicatorSubCategoryName)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.IndicatorTypeCode)
              .NotEmpty()
              .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
