using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using FluentValidation.Attributes;

namespace ODMSModel.MaintenanceAppointment
{
    [Validator(typeof(MaintenanceAppointmentViewModelValidator))]
    public class MaintenanceAppointmentViewModel : ModelBase
    {
        public MaintenanceAppointmentViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        private int _dealerId { get; set; }
        public int MaintenanceId { get; set; }
        public int AppointmentId { get; set; }

        [Display(Name = "Appointment_Display_CustomerSelect", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? CustomerId { get; set; }

        [Display(Name = "Appointment_Display_VehicleSearch", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? VehicleId { get; set; }

        [Display(Name = "Appointment_Display_AppointmentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? AppointmentTypeId { get; set; }

        [Display(Name = "Appointment_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int DealerId { get { return ODMSCommon.Security.UserManager.UserInfo.IsDealer ? ODMSCommon.Security.UserManager.UserInfo.GetUserDealerId() : _dealerId; } set { _dealerId = value; } }

        public string VehicleModelName { get; set; }
        public string VehicleTypeName { get; set; }

        [Display(Name = "Appointment_Display_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }

        [Display(Name = "Appointment_Display_AppointmentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentTypeName { get; set; }

        [Display(Name = "Appointment_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Appointment_Display_Vehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleIdVehiclePlate { get; set; }

        [Display(Name = "Appointment_Display_AppointmentDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentDateFormatted
        {
            get
            {
                return AppointmentDate.GetValueOrDefault().ToString(new CultureInfo(CultureInfo.CurrentCulture.Name));
            }
            set { }
        }

        [Display(Name = "Appointment_Display_AppointmentTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppointmentTimeFormatted
        {
            get
            {
                return !AppointmentTime.HasValue ? string.Empty : string.Format("{0}:{1}",
                    AppointmentTime.Value.Hours < 10 ? "0" + AppointmentTime.Value.Hours.ToString() : AppointmentTime.Value.Hours.ToString(),
                    AppointmentTime.Value.Minutes == 0 ? "00" : AppointmentTime.Value.Minutes.ToString());
            }
            set { }
        }


        [Display(Name = "Appointment_Display_ContactName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactName { get; set; }
        [Display(Name = "Appointment_Display_ContactSurname", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactLastName { get; set; }
        [Display(Name = "Appointment_Display_ContactMobilePhone", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactPhone { get; set; }
        [Display(Name = "Appointment_Display_ContactAddress", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactAddress { get; set; }
        [Display(Name = "Appointment_Display_VehiclePlate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehiclePlate { get; set; }
        [Display(Name = "Appointment_Display_VehicleColor", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleColor { get; set; }
        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelCode { get; set; }
        [Display(Name = "Appointment_Display_VehicleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleType { get; set; }
        public string VehicleTypeId { get; set; }
        [Display(Name = "Appointment_Display_AppointmentDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? AppointmentDate { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Appointment_Display_AppointmentTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public TimeSpan? AppointmentTime { get; set; }

        [Display(Name = "Appointment_Display_ComplaintDescription", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ComplaintDescription { get; set; }

        [Display(Name = "Appointment_Display_DeliveryEstimateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DeliveryEstimateDate { get; set; }

        [Display(Name = "Appointment_Display_DeliveryEstimateTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public TimeSpan? DeliveryEstimateTime { get; set; }

        [Display(Name = "Appointment_Display_VehicleVin", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleVin { get; set; }
    }
}
