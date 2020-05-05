using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CampaignRequestApprove
{
    public class CampaignRequestApproveViewModelValidator : AbstractValidator<CampaignRequestApproveViewModel>
    {
        public CampaignRequestApproveViewModelValidator()
        {
            RuleFor(c => c.CampaignRequestId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.RequestStatusId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.RequestNote).Length(0, 200)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.SupplierDealerId)
                .NotNull()
                .When(c => c.SupplierTypeId == 0)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
