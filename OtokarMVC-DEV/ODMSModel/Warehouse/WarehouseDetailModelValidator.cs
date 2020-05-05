using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.Warehouse
{
    public class WarehouseDetailModelValidator : AbstractValidator<WarehouseDetailModel>
    {
        public WarehouseDetailModelValidator()
        {
            RuleFor(f => f.Code)
                .NotEmpty()
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Code)
                .Length(0, 5)
                .WithMessage(string.Format(MessageResource.Validation_Length, 5));
        }
    }
}
