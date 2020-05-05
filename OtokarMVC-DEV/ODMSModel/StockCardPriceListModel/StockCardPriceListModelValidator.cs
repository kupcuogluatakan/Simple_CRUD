using FluentValidation;

namespace ODMSModel.StockCardPriceListModel
{
    public class StockCardPriceListModelValidator : AbstractValidator<StockCardPriceListModel>
    {
        public StockCardPriceListModelValidator()
        {
            RuleFor(c => c.StockCardId).NotEqual(0).WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.PartId).NotEqual(0).WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.CompanyPrice).NotEqual(0).WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.CostPrice).NotEqual(0).WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.ListPrice).NotEqual(0).WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            RuleFor(c => c.PriceList).NotNull().WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);            
        }
    }
}
