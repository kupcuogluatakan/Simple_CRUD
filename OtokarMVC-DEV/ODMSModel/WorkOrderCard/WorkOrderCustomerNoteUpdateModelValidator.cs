using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderCustomerNoteUpdateModelValidator: AbstractValidator<WorkOrderCustomerNoteUpdateModel>
    {
        public WorkOrderCustomerNoteUpdateModelValidator()
        {
            RuleFor(c => c.WorkOrderId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Note).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Note)
                .Length(1, 500)
                .WithMessage(string.Format(MessageResource.Validation_Length, 500));
        }
    }
}
