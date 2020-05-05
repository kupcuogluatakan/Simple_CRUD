using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CampaignDismantlePart
{
    public class CampaignDismantlePartModelValidator : AbstractValidator<CampaignDismantlePartViewModel>
    {
        public CampaignDismantlePartModelValidator()
        {
            //PartId
            RuleFor(m => m.PartId)
               .NotNull()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Note)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
        }
    }
}
