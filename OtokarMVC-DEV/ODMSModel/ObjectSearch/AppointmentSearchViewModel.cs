using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.ObjectSearch
{
    public class AppointmentSearchViewModel:ObjectSearchModel
    {
        [Display(Name = "AppointmentSearch_Display_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "AppointmentSearch_Display_ContactName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactName { get; set; }
        [Display(Name = "Appointment_Display_VehiclePlate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Plate { get; set; }
        [Display(Name = "Appointment_Display_AppointmentDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
