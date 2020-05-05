using FluentValidation;

namespace ODMSModel.WorkOrderCard
{

    public class WorkOrderQuantityDataModelValidator : AbstractValidator<WorkOrderQuantityDataModel>
    {
        public WorkOrderQuantityDataModelValidator()
        {
            RuleFor(c => c.WorkOrderId).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.ItemId).NotEmpty();
            RuleFor(c => c.WorkOrderDetailId).NotEmpty();
            RuleFor(c => c.Quantity).NotEmpty();
            RuleFor(c => c.Type).Must(c => c == "PART" || c == "LABOUR");
        }
    }
}
