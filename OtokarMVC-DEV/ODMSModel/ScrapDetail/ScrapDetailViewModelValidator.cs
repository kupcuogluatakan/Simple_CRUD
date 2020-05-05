using FluentValidation;

namespace ODMSModel.ScrapDetail
{
    public class ScrapDetailViewModelValidator : AbstractValidator<ScrapDetailViewModel>
    {
        public ScrapDetailViewModelValidator()
        {
            RuleFor(c => c.ScrapDate)
                .NotNull()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.ScrapReasonId)
                .NotNull()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.ScrapId)
                .NotNull()
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.Barcode)
                .NotNull().When(c=>c.ScrapId !=0)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.PartId)
                .NotNull().When(c => c.ScrapId != 0)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.StockTypeId)
                .NotNull().When(c => c.ScrapId != 0)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);
            
            RuleFor(c => c.WarehouseId)
                .NotNull().When(c => c.ScrapId != 0)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.RackId)
                .NotNull().When(c => c.ScrapId != 0)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

            RuleFor(c => c.Quantity)
                .NotNull().When(c => c.ScrapId != 0)
                .WithLocalizedMessage(() => ODMSCommon.Resources.MessageResource.Validation_Required);

        }
    }
}
