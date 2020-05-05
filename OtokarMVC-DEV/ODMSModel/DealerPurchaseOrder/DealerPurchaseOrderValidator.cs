using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.DealerPurchaseOrder
{
    public class DealerPurchaseOrderValidator : AbstractValidator<DealerPurchaseOrderViewModel>
    {
        public DealerPurchaseOrderValidator()
        {
            RuleFor(c => c.SupplierId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
   
            RuleFor(c => c.PurchaseOrderType).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        
        }
    }
}
