using FluentValidation.Attributes;

namespace ODMSModel.WorkOrderCard
{
    [Validator(typeof(WorkOrderQuantityDataModelValidator))]
    public class WorkOrderQuantityDataModel:ModelBase
    {
        public long WorkOrderId { get; set; }
        public long WorkOrderDetailId { get; set; }
        public string Type { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Duration { get; set; }
        public bool LabourDealerDurationCheck { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
