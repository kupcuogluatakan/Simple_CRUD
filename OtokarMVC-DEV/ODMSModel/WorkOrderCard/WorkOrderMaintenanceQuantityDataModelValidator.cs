using FluentValidation;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderMaintenanceQuantityDataModelValidator : AbstractValidator<WorkOrderMaintenanceQuantityDataModel>
    {
        public WorkOrderMaintenanceQuantityDataModelValidator()
        {
            RuleFor(c => c.WorkOrderId).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.ItemId).NotEmpty();
            RuleFor(c => c.WorkOrderDetailId).NotEmpty();
            RuleFor(c => c.MaintenanceId).NotEmpty();
            RuleFor(c => c.Type).Must(c => c == "PART" || c == "LABOUR");
        }
    }
}
