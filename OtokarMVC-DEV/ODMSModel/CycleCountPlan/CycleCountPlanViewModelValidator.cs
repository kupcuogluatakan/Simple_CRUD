using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.CycleCountPlan
{
    public class CycleCountPlanViewModelValidator : AbstractValidator<CycleCountPlanViewModel>
    {
        public CycleCountPlanViewModelValidator()
        {
            RuleFor(c => c.WarehouseId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.CycleCountId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
