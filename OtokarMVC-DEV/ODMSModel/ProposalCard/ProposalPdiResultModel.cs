using FluentValidation;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    [Validator(typeof(ProposalPdiResultModelValidator))]
    public class ProposalPdiResultModel
    {
        public long ProposalId { get; set; }
        public string ControlCode { get; set; }
        public string PartCode { get; set; }
        public string BreakDownCode { get; set; }
        public string ResultCode { get; set; }
    }

    public class ProposalPdiResultModelValidator : AbstractValidator<ProposalPdiResultModel>
    {
        public ProposalPdiResultModelValidator()
        {
            RuleFor(c => c.ProposalId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ControlCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.PartCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.BreakDownCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ResultCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
