
namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderMaintenanceModel:ModelBase
    {
        public long WorkOrderId { get; set; }
        public long WorkOrderDetailId { get; set; }
        public int MaintenanceId { get; set; }
        public string MaintenancName { get; set; }
        public int MaintenanceKilometer { get; set; }
        public int VehicleKilometer { get; set; }
    }
}
