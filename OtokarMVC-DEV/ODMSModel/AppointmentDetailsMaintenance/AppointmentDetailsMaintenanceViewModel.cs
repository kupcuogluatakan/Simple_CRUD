using System.ComponentModel.DataAnnotations;

namespace ODMSModel.AppointmentDetailsMaintenance
{
    public class AppointmentDetailsMaintenanceViewModel : ModelBase
    {
        [Display(Name = "AppointmentDetailsMaint_Display_MaintNew", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? MaintId { get; set; }

        public int AppIndicId { get; set; }

        public int AppId { get; set; }

        [Display(Name = "Maintenance_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Name { get; set; }

        public bool IsRemoved { get; set; }

        public int Quantity { get; set; }

        public int LabourPartId { get; set; }

        public string Type { get; set; }

        public int ObjType { get; set; }

        public bool IsMust { get; set; }
        public string CategoryString { get; set; }
    }
}
