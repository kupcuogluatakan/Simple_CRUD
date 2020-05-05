using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartSaleDetail
{
    public class OSparePartSaleDetailViewModelValidator : AbstractValidator<OSparePartSaleDetailViewModel>
    {
        public OSparePartSaleDetailViewModelValidator()
        {
            RuleFor(c => c.ReturnReasonText).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Price).NotNull().When(c=>c.StockTypeId == 1).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Price)
                .NotEqual(0).When(c => c.StockTypeId == 1)
                .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PlanQuantity).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
