using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartGuaranteeAuthorityNeed
{
    public class SparePartGuaranteeAuthorityNeedViewModelValidator : AbstractValidator<SparePartGuaranteeAuthorityNeedViewModel>
    {
        public SparePartGuaranteeAuthorityNeedViewModelValidator()
        {
            RuleFor(c => c.PartId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.GuaranteeAuthorityNeed).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
