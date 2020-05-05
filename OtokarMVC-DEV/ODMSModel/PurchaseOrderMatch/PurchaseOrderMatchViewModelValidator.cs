using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.PurchaseOrderMatch
{
    public class PurchaseOrderMatchViewModelValidator : AbstractValidator<PurchaseOrderMatchViewModel>
    {
        public PurchaseOrderMatchViewModelValidator()
        {
            RuleFor(x => x.PurhcaseOrderGroupId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(x => x.PurhcaseOrderTypeId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(x => x.SalesOrganization).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(x => x.SalesOrganization).Length(0, 10).WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(x => x.DistrChan).Length(0, 5).When(x => x.DistrChan != null).WithLocalizedMessage(() => MessageResource.Validation_Length, 5);
            RuleFor(x => x.Division).Length(0, 5).When(x => x.Division != null).WithLocalizedMessage(() => MessageResource.Validation_Length, 5);
        }
    }
}
