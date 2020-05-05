using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.SupplierDispatchPart
{
    public class SupplierDispatchPartOrderViewModelValidator : AbstractValidator<SupplierDispatchPartOrderViewModel>
    {
        public SupplierDispatchPartOrderViewModelValidator()
        {            
            RuleFor(c => c.PurchaseNo).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
