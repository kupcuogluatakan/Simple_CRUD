using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Kendo.Mvc.UI;
using System.Text;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.Schedule
{
    public class ScheduleViewModel : ModelBase, ISchedulerEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }

        public bool WorkStat { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public DateTime WorkHourStart { get; set; }
        public DateTime WorkHourEnd { get; set; }
        public DateTime LunchBreakStart { get; set; }
        public DateTime LunchBreakEnd { get; set; }
        public DateTime CurrentWeek { get; set; }
        public bool isToday { get; set; }
        public int Qty { get; set; }
        public string LangCode { get { return ODMSCommon.Security.UserManager.LanguageCode; } }

        public int? AppointmentId { get; set; }

        public string NonAppId { get; set; }

        [Display(Name = "Appointment_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public new int? StatusId { get; set; }

        [Display(Name = "Appointment_Display_OptionValue", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OptionValue { get; set; }

        [Display(Name = "Appointment_Display_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? CustomerId { get; set; }

        [Display(Name = "Appointment_Display_Vehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? VehicleId { get; set; }

        public string VinNo { get; set; }

        [Display(Name = "Appointment_Display_AppointmentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? AppointmentTypeId { get; set; }

        [Display(Name = "Appointment_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int DealerId { get; set; }

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
                return AppointmentDate.ToString(new CultureInfo(CultureInfo.CurrentCulture.Name));
            }
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
        }

        [Display(Name = "Appointment_Display_ContactName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactName { get; set; }
        [Display(Name = "Appointment_Display_ContactName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactFirstName { get; set; }

        [Display(Name = "Appointment_Display_ContactSurname", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactLastName { get; set; }

        [Display(Name = "Appointment_Display_ContactPhone", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
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

        [Required(ErrorMessage = "*")]
        [Display(Name = "Appointment_Display_AppointmentDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Time)]
        [Display(Name = "Appointment_Display_AppointmentTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public TimeSpan? AppointmentTime { get; set; }

        [Display(Name = "Appointment_Display_ComplaintDescription", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ComplaintDescription { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Appointment_Display_AppointmentStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime AppointmentStartDate { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Appointment_Display_AppointmentStartTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        [DataType(DataType.Time)]
        public TimeSpan? AppointmentStartTime { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Appointment_Display_AppointmentEndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime AppointmentEndDate { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Appointment_Display_AppointmentEndTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        [DataType(DataType.Time)]
        public TimeSpan? AppointmentEndTime
        { get; set; }

        [Display(Name = "Appointment_Display_DeliveryEstimateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DeliveryEstimateDateString { get; set; }

        [Display(Name = "Appointment_Display_DeliveryEstimateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DeliveryEstimateDate { get; set; }

        [Display(Name = "Appointment_Display_DeliveryEstimateTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public TimeSpan? DeliveryEstimateTime { get; set; }

        [Display(Name = "Appointment_Display_VehicleVin", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleVin { get; set; }

        public bool IsValid(ScheduleViewModel model)
        {
            bool result = true;
            StringBuilder sb = new StringBuilder();
            if (model.OptionValue == ((int)SchedulerOptionValue.Appointment).ToString())
            {

                DateTime startDate = new DateTime();
                startDate = model.AppointmentDate.ToShortDateString().GetValue<DateTime>().Date + model.AppointmentTime.Value;


                if (startDate < DateTime.Now)
                {
                    model.ErrorNo = 1;
                    sb.Append(MessageResource.ScheduleViewModel_Display_ValidationDate);
                    result = false;
                }

                if (startDate.ToShortDateString().GetValue<DateTime>() == DateTime.Now.ToShortDateString().GetValue<DateTime>() && TimeSpan.Parse(startDate.ToString("HH:mm")) < TimeSpan.Parse(DateTime.Now.ToString("HH:mm")))
                {
                    model.ErrorNo = 1;
                    sb.Append(string.Format(MessageResource.ScheduleViewModel_Display_ValidationTime, DateTime.Now.Hour, DateTime.Now.Minute));
                    result = false;
                }
            }
            else if (model.OptionValue == ((int)SchedulerOptionValue.UnAppoinment).ToString())
            {

                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();

                startDate = model.AppointmentStartDate.ToShortDateString().GetValue<DateTime>().Date + model.AppointmentStartTime.Value;
                endDate = model.AppointmentEndDate.ToShortDateString().GetValue<DateTime>().Date + model.AppointmentEndTime.Value;


                if (startDate > endDate)
                {
                    model.ErrorNo = 1;
                    sb.Append(MessageResource.ScheduleViewModel_Display_ValidationDateCondition);
                    result = false;
                }

                if (startDate < DateTime.Now || endDate < DateTime.Now)
                {
                    model.ErrorNo = 1;
                    sb.Append(MessageResource.ScheduleViewModel_Display_ValidationDateClose);
                    result = false;
                }

                if ((startDate.ToShortDateString().GetValue<DateTime>() == DateTime.Now.ToShortDateString().GetValue<DateTime>() &&
                    endDate.ToShortDateString().GetValue<DateTime>() == DateTime.Now.ToShortDateString().GetValue<DateTime>()) &&
                    (TimeSpan.Parse(startDate.ToString("HH:mm")) < TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) ||
                    TimeSpan.Parse(endDate.ToString("HH:mm")) < TimeSpan.Parse(DateTime.Now.ToString("HH:mm"))))
                {
                    model.ErrorNo = 1;
                    sb.Append(string.Format(MessageResource.ScheduleViewModel_Display_ValidationTime, DateTime.Now.Hour, DateTime.Now.Minute));
                    result = false;
                }
            }

            var message = !string.IsNullOrEmpty(sb.ToString()) ? sb.ToString().Substring(0, sb.ToString().Length - 1) : string.Empty;
            model.ErrorMessage = message;
            return result;
        }
    }

    public enum SchedulerOptionValue
    {
        Appointment = 1,
        UnAppoinment = 2,
        UnDefined = 3
    }
}
