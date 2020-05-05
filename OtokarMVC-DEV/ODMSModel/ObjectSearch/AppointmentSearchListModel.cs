using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.ObjectSearch
{
    public class AppointmentSearchListModel:BaseListWithPagingModel
    {
        public AppointmentSearchListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CustomerFullName", "CUSTOMER_FULL_NAME"},
                    {"AppointmentDate", "APPOINTMENT_DATE"},
                    {"ContactName", "CONTACT_NAME"},
                    {"ContactSurName", "CONTACT_SURNAME"},
                    {"ContactAddress", "CONTACT_ADDRESS"},
                    {"ContactPhone", "CONTACT_PHONE"},
                    {"VehiclePlate", "VEHICLE_PLATE"},
                    {"VehicleColor", "VEHICLE_COLOR"},
                    {"AppointmentType", "APPOINTMENT_TYPE"},
                    {"VehicleModel", "MODEL_NAME"},
                };
            SetMapper(dMapper);
        }

        public AppointmentSearchListModel(){ }


        [Display(Name = "Appointment_Display_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerFullName { get; set; }
        [Display(Name = "Appointment_Display_AppointmentDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime AppointmentDate { get; set; }
        [Display(Name = "Appointment_Display_ContactName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactName { get; set; }
        [Display(Name = "Appointment_Display_ContactSurname", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactSurname{ get; set; }
        [Display(Name = "Appointment_Display_ContactAddress", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactAddress { get; set; }
        [Display(Name = "Appointment_Display_ContactPhone", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactPhone { get; set; }
        [Display(Name = "Appointment_Display_VehiclePlate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehiclePlate { get; set; }
        [Display(Name = "Appointment_Display_VehicleColor", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleColor { get; set; }
        [Display(Name = "Appointment_Display_AppointmentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string  AppointmentType { get; set; }
        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModel { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate{ get; set; }

        public int AppointmentId { get; set; }
    }
}
