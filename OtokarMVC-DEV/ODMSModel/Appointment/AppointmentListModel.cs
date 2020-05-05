using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Appointment
{
    public class AppointmentListModel : BaseListWithPagingModel
    {
        public AppointmentListModel()
        {

        }

        public AppointmentListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"CustomerFullName", "CUSTOMER_FULL_NAME"},
                     {"VehicleType", "VEHICLE_TYPE"},
                     {"VehiclePlate", "VEHICLE_PLATE"},
                     {"Vehicle", "PLATE"},
                     {"AppointmentTypeName", "APPOINTMENT_TYPE_NAME"},
                     {"AppointmentDate", "APPOINTMENT_DATE"},
                     {"AppointmentTime", "APPOINTMENT_TIME"},
                     {"AppStatus","APPOINTMENT_STATUS"}
                 };
            SetMapper(dMapper);
        }
        public int DealerId { get; set; }
        public int AppointmentTypeId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime? AppointmentDateEnd { get; set; }
        [Display(Name = "Appointment_Display_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerFullName { get; set; }
        [Display(Name = "Appointment_Display_AppointmentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentTypeName { get; set; }
        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "Appointment_Display_VehicleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "Appointment_Display_VehiclePlate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehiclePlate { get; set; }
        [Display(Name = "Appointment_Display_Vehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Vehicle { get; set; }
        [Display(Name = "Appointment_Display_AppointmentDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? AppointmentDate { get; set; }
        [Display(Name = "Appointment_Display_AppointmentDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentDateFormatted { get; set; }
        [Display(Name = "Appointment_Display_AppointmentTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentTime { get; set; }

        public int AppointmentStatus { get; set; }
        [Display(Name = "Appointment_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppStatus { get; set; }
    }
}
