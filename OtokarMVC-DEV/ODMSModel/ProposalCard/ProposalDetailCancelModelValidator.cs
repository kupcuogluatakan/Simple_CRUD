using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalDetailCancelModelValidator:AbstractValidator<ProposalDetailCancelModel>
    {
        public ProposalDetailCancelModelValidator()
        {
            RuleFor(c => c.ProposalId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.ProposalDetailId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CancelReason).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.CancelReason)
                .Length(1, 500)
                .WithMessage(string.Format(MessageResource.Validation_Length, 500));
        }
    }
}
