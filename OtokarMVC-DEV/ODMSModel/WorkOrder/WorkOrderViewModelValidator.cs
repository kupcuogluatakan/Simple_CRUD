using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrder
{
    public class WorkOrderViewModelValidator : AbstractValidator<WorkOrderViewModel>
    {
        public WorkOrderViewModelValidator()
        {
            RuleFor(c => c.AppointmentTypeId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.VehicleId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Stuff).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Note).Length(0, 500).WithMessage(string.Format(MessageResource.Validation_Length, 500));
            RuleFor(c => c.Note).NotEmpty().WithLocalizedMessage(()=>MessageResource.Validation_Required);
        }
    }
}
