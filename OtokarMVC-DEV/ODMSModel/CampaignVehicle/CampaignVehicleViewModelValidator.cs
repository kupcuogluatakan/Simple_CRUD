using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CampaignVehicle
{
    public class CampaignVehicleViewModelValidator : AbstractValidator<CampaignVehicleViewModel>
    {
        public CampaignVehicleViewModelValidator()
        {
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsUtilized).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
