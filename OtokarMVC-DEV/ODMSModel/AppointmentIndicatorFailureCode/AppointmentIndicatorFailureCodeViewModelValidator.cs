using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.AppointmentIndicatorFailureCode
{
    public class AppointmentIndicatorFailureCodeViewModelValidator : AbstractValidator<AppointmentIndicatorFailureCodeViewModel>
    {
        public AppointmentIndicatorFailureCodeViewModelValidator()
        {
            RuleFor(c => c.AdminDesc)
                .Length(0, 30)
               .WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(c => c.Code).Length(0, 3).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.Code)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);

            RuleFor(m => m.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(m => m.DescriptionML)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
