using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartSaleOrder
{
    public class SparePartSaleOrderViewModelValidator : AbstractValidator<SparePartSaleOrderViewModel>
    {
        public SparePartSaleOrderViewModelValidator()
        {
            //required
            RuleFor(c => c.CustomerId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SoTypeId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
