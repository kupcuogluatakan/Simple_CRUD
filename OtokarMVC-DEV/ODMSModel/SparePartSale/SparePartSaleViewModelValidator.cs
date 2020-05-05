using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartSale
{
    public class SparePartSaleViewModelValidator : AbstractValidator<SparePartSaleViewModel>
    {
        public SparePartSaleViewModelValidator()
        {
            //required
            RuleFor(c => c.CustomerId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SaleDate).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StatusId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StockTypeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
} 
