using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.DamagedItemDispose
{
    public class DamagedItemDisposeViewModelValidator : AbstractValidator<DamagedItemDisposeViewModel>
    {
        public DamagedItemDisposeViewModelValidator()
        {
            RuleFor(c => c.DealerId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity).NotEqual(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StockTypeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.RackId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Description)
                .Length(0, 100)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
        }
    }
}
