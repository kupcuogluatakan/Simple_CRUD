using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CampaignDocument
{
    public class CampaignDocumentViewModelValidator : AbstractValidator<CampaignDocumentViewModel>
    {
        public CampaignDocumentViewModelValidator()
        {
            RuleFor(c => c.CampaignCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CampaignCode)
                .Length(0, 8)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.LanguageCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.LanguageCode)
                .Length(0, 4)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.DocumentDesc).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DocumentDesc)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
        }
    }
}
