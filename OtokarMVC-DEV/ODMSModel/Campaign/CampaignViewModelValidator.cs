using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.Campaign
{
    public class CampaignViewModelValidator : AbstractValidator<CampaignViewModel>
    {
        public CampaignViewModelValidator()
        {
            RuleFor(c => c.CampaignCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CampaignCode)
                .Length(0, 8)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.AdminDesc).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.AdminDesc)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(m => m.MainFailureCode)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ModelKod).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ModelKod)
                .Length(0, 19)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsValidAbroad).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.MultiLanguageContentAsText)
                .Must(CommonUtility.IsTurkishContentFilled)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StartDate).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.EndDate).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SubCategoryId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.EndDate)
                .GreaterThan(o => o.StartDate)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                                           MessageResource.Campaign_Display_EndDate,
                                           MessageResource.Campaign_Display_StartDate));
        }
    }
}
