using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CampaignRequestOrders
{
    class CampaignRequestOrdersModelValidator : AbstractValidator<CampaignRequestOrdersModel>
    {
        public CampaignRequestOrdersModelValidator()
        {
            RuleFor(c => c.CampaignCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CampaignCode)
                .Length(0, 8)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.Quantity).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.status).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.RequestDate).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            
        }
    }
}
