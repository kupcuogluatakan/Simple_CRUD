using System;
using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.MaintenanceAppointment
{
    public class MaintenanceAppointmentViewModelValidator : AbstractValidator<MaintenanceAppointmentViewModel>
    {
        public MaintenanceAppointmentViewModelValidator()
        {
            RuleFor(q => q.AppointmentDate)
               .NotEmpty()
               .WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(x => x.AppointmentDate)
                 .Must((m, o) => m.AppointmentDate >= DateTime.Now)
                 .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                 MessageResource.Appointment_Display_AppointmentDate, DateTime.Now));
            RuleFor(q => q.ComplaintDescription)
                .Length(0, 2000)
                .WithMessage(string.Format(MessageResource.Validation_Length, 2000));
            RuleFor(q => q.ContactName)
                .Length(0, 30)
                .WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(q => q.ContactLastName)
                .Length(0, 30)
                .WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(q => q.ContactPhone)
                .Length(0, 20)
                .WithMessage(string.Format(MessageResource.Validation_Length, 20));
            RuleFor(q => q.ContactAddress)
                .Length(0, 50)
                .WithMessage(string.Format(MessageResource.Validation_Length, 50));
            RuleFor(q => q.VehiclePlate)
                .Length(0, 20)
                .WithMessage(string.Format(MessageResource.Validation_Length, 20));
            RuleFor(q => q.VehicleColor)
                .Length(0, 20)
                .WithMessage(string.Format(MessageResource.Validation_Length, 20));
        }
    }
}
