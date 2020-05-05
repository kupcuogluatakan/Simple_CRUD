using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.PurchaseOrder
{
    public class PurchaseOrderViewModelValidator : AbstractValidator<PurchaseOrderViewModel>
    {
        public PurchaseOrderViewModelValidator()
        {
            RuleFor(c => c.IdDealer).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdStockType).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.VehicleId).NotEmpty().When(x=>x.IsVehicleSelectionMust.GetValueOrDefault()).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.VehicleId).NotNull().When(x => x.IsVehicleSelectionMust.GetValueOrDefault()).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SupplyType).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdSupplier).NotEmpty().When(x => x.SupplyType == 2).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PoType).NotEmpty().When(x => x.SupplyType == 1).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsBranchOrder).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdDefect).NotNull().When(x => x.PoType == (int)CommonValues.PurchaseOrderType.Contract).WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
