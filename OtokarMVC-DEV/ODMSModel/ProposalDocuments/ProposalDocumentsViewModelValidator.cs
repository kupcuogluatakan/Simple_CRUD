using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalDocuments
{
    public class ProposalDocumentsViewModelValidator : AbstractValidator<ProposalDocumentsViewModel>
    {
        public ProposalDocumentsViewModelValidator()
        {
            //Description
            RuleFor(q => q.Description)
                .Length(0, 250)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(q => q.Description).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(q => q.file).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
