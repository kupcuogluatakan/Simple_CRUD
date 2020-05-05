using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.Appointment
{
    public class AppointmentViewModelValidator : AbstractValidator<AppointmentViewModel>
    {
        public AppointmentViewModelValidator()
        {
            RuleFor(v => v.DealerId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.AppointmentTypeId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.AppointmentDate).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(v => v.ComplaintDescription).NotEmpty().WithMessage(MessageResource.Validation_Required);
            
            RuleFor(v => v.ComplaintDescription).Length(0,2000).WithMessage(string.Format(MessageResource.Validation_Length, 2000));
            RuleFor(v => v.ContactName).Length(0, 100).WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(v => v.ContactLastName).Length(0, 50).WithMessage(string.Format(MessageResource.Validation_Length, 30));
            RuleFor(v => v.ContactPhone).Length(0, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20));
            RuleFor(v => v.ContactAddress).Length(0, 800).WithMessage(string.Format(MessageResource.Validation_Length, 50));
            RuleFor(v => v.VehiclePlate).Length(0, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20));
            RuleFor(v => v.VehicleColor).Length(0, 20).WithMessage(string.Format(MessageResource.Validation_Length, 20));
        }
    }
}
