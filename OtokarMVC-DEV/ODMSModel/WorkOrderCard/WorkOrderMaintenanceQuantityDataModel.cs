using FluentValidation.Attributes;

namespace ODMSModel.WorkOrderCard
{
    [Validator(typeof(WorkOrderMaintenanceQuantityDataModelValidator))]
    public class WorkOrderMaintenanceQuantityDataModel : WorkOrderQuantityDataModel
    {
        public bool IsMust { get; set; }
        public bool DiffrentBrandAllowed { get; set; }
        public bool AlternateAllowed { get; set; }
        public int MaintenanceId { get; set; }
        public string NewPartId { get; set; }
    }
}
