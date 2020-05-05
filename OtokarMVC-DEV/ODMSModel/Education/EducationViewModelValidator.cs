using FluentValidation;
using ODMSCommon.Resources;
using ODMSCommon;

namespace ODMSModel.Education
{
    public class EducationViewModelValidator : AbstractValidator<EducationViewModel>
    {
        public EducationViewModelValidator()
        {

            //VehicleModelCode
            RuleFor(q => q.VehicleModelCode)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            //EducationTypeId
            RuleFor(q => q.EducationTypeId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            //EducationCode
            RuleFor(q => q.EducationCode)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.EducationCode)
                .Length(0, 10)
                .WithMessage(string.Format(MessageResource.Validation_Length, 10));
            //AdminDesc
            RuleFor(q => q.AdminDesc)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.AdminDesc)
                .Length(0, 100)
                .WithMessage(string.Format(MessageResource.Validation_Length, 100));
            //EducationName
            RuleFor(q => q.EducationName)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            //Duration Day
            RuleFor(q => q.EducationDurationDay)
              .NotEmpty()
              .WithLocalizedMessage(() => MessageResource.Validation_Required);

            //Duration Hour
            RuleFor(q => q.EducationDurationHour)
             .NotEmpty()
             .WithLocalizedMessage(() => MessageResource.Validation_Required);

        }
    }
}
