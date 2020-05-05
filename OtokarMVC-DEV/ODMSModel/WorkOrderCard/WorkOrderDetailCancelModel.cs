using FluentValidation.Attributes;

namespace ODMSModel.WorkOrderCard
{
    [Validator(typeof(WorkOrderDetailCancelModelValidator))]
    public class WorkOrderDetailCancelModel:ModelBase
    {
        public long WorkOrderId { get; set; }
        public long WorkOrderDetailId { get; set; }
        public string CancelReason { get; set; }
    }

}
