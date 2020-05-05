using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Proposal
{
    public class ProposalViewModelValidator : AbstractValidator<ProposalViewModel>
    {
        public ProposalViewModelValidator()
        {
            RuleFor(c => c.AppointmentTypeId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.VehicleId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Stuff).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Note).Length(0, 500).WithMessage(string.Format(MessageResource.Validation_Length, 500));
            RuleFor(c => c.Note).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
