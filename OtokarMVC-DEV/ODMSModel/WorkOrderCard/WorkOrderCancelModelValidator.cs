using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderCancelModelValidator : AbstractValidator<WorkOrderCancelModel>
    {
        public WorkOrderCancelModelValidator()
        {
            RuleFor(c => c.WorkOrderId).NotEmpty().WithMessage(MessageResource.Validation_Required);

            RuleFor(c => c.CancelReason).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CancelReason)
                .Length(1, 500)
                .WithMessage(string.Format(MessageResource.Validation_Length, 500));
        }
    }
}
