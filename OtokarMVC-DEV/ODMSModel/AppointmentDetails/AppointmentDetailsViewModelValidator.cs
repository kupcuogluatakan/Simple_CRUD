using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.AppointmentDetails
{
    public class AppointmentDetailsViewModelValidator : AbstractValidator<AppointmentDetailsViewModel>
    {
        public AppointmentDetailsViewModelValidator()
        {
            RuleFor(c => c.AppointmentId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AppointmentId)
                .NotEqual(0)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AppointmentIndicatorId)
                .NotNull()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MainCategoryId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MainCategoryId)
                .NotEqual(0)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CategoryId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CategoryId)
                .NotEqual(0)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SubCategoryId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SubCategoryId)
                .NotEqual(0)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IndicatorTypeCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CampaignCode)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MaintKId)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MaintPId)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.FailureCodeId)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
