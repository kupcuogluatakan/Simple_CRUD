using System;
using System.ComponentModel.DataAnnotations;
namespace ODMSModel.Appointment
{
    public class AppointmentIndexViewModel : ModelBase
    {
        private int _dealerId { get; set; }
        [Display(Name = "Appointment_Display_AppointmentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? AppointmentTypeId { get; set; }
        [Display(Name = "Appointment_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int DealerId { get { return ODMSCommon.Security.UserManager.UserInfo.IsDealer ? ODMSCommon.Security.UserManager.UserInfo.GetUserDealerId() : _dealerId; } set { _dealerId = value; } }
        [Display(Name = "Appointment_Display_VehiclePlate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehiclePlate { get; set; }
        [Display(Name = "Appointment_Display_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactName { get; set; }
        [Display(Name = "Appointment_Display_AppointmentDateStart", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? AppointmentDateStart { get; set; }
        [Display(Name = "Appointment_Display_AppointmentDateEnd", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? AppointmentDateEnd { get; set; }
        [Display(Name = "Appointment_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StatusId { get; set; }

    }
}
