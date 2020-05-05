using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.MaintenanceAppointment
{
    public class MaintenanceAppointmentListModel : BaseListWithPagingModel
    {
        public MaintenanceAppointmentListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"MaintenanceId", "MAINTENANCE_ID"},
                     {"MaintModelKod", "MAINT_MODEL_KOD"},
                     {"VehicleModelKod", "VEHICLE_MODEL_KOD"},
                     {"VehicleVehicleTypeId", "VEHICLE_VEHICLE_TYPE_ID"},
                     {"MaintVehicleTypeId", "MAINT_VEHICLE_TYPE_ID"},
                     {"VehicleId", "VEHICLE_ID"},
                     {"VehicleTypeName", "TYPE_NAME"},
                     {"VinNo", "VIN_NO"},
                     {"Plate", "PLATE"},
                     {"Price", "12"},
                     {"MaintKM", "MAINT_KM"},
                     {"MaintMonth", "MAINT_MONTH"},
                     {"AdminDesc", "ADMIN_DESC"}
                 };
            SetMapper(dMapper);

        }

        public MaintenanceAppointmentListModel()
        {
        }

        public int MaintenanceId { get; set; }
        [Display(Name = "Vehicle_Display_Plate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Plate { get; set; }

        public string MaintenanceModelKod { get; set; }

        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelKod { get; set; }

        public int VehicleVehicleTypeId { get; set; }
        public int MaintenanceVehicleTypeId { get; set; }
        public int VehicleId {get;set;}

        public int VehicleTypeId { get; set; }
        [Display(Name = "VehicleType_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleTypeName { get; set; }

        [Display(Name = "Maintenance_Display_Month", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MaintMonth { get; set; }

        [Display(Name = "MaintenanceAppointment_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Maintenance_Display_Km", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MaintKM { get; set; }

        [Display(Name = "MaintenanceAppointment_Display_Price", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Price { get; set; }

        public bool IsVisible { get { return VehicleModelKod == MaintenanceModelKod; } }
        
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
    }
}
