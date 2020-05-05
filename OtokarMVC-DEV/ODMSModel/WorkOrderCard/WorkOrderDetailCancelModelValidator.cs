using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderDetailCancelModelValidator : AbstractValidator<WorkOrderDetailCancelModel>
    {
        public WorkOrderDetailCancelModelValidator()
        {
            RuleFor(c => c.WorkOrderId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.WorkOrderDetailId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CancelReason).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CancelReason)
                .Length(1, 500)
                .WithMessage(string.Format(MessageResource.Validation_Length, 500));
        }
    }
}
