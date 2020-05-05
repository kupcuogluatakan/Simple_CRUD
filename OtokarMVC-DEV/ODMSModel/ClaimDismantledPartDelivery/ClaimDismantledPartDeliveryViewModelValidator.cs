using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.ClaimDismantledPartDelivery
{
    public class ClaimDismantledPartDeliveryViewModelValidator : AbstractValidator<ClaimDismantledPartDeliveryViewModel>
    {
        public ClaimDismantledPartDeliveryViewModelValidator()
        {
            RuleFor(c => c.ClaimWayBillNo).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ClaimWayBillNo).Length(0, 15).WithLocalizedMessage(() => MessageResource.Validation_Length, 15);

            RuleFor(c => c.ClaimWayBillSerialNo).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ClaimWayBillSerialNo).Length(0, 10).WithLocalizedMessage(() => MessageResource.Validation_Length, 10);

            RuleFor(c => c.ClaimWayBillDate).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
