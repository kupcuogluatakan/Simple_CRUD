using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CampaignRequest
{
    public class CampaignRequestViewModelValidator : AbstractValidator<CampaignRequestViewModel>
    {
        public CampaignRequestViewModelValidator()
        {
            RuleFor(v => v.RequestNote).Length(0, 1000).WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.CampaignCode).Length(0, 8).WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.CampaignCode).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdDealer).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);               
        }
    }
}
