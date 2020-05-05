using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.PurchaseOrderGroup
{
    public class PurchaseOrderGroupViewModelValidator : AbstractValidator<PurchaseOrderGroupViewModel>
    {
        public PurchaseOrderGroupViewModelValidator()
        {
            RuleFor(c => c.GroupName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.GroupName).Length(0, 100).WithLocalizedMessage(() => MessageResource.Validation_Length, 100);
        }
    }
}
