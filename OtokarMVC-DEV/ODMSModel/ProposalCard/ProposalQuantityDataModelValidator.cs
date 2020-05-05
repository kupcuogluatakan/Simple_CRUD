using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalQuantityDataModelValidator:AbstractValidator<ProposalQuantityDataModel>
    {
        public ProposalQuantityDataModelValidator()
        {
            RuleFor(c => c.ProposalId).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.ItemId).NotEmpty();
            RuleFor(c => c.ProposalDetailId).NotEmpty();
            RuleFor(c => c.Quantity).NotEmpty();
            RuleFor(c => c.Type).Must(c => c == "PART" || c == "LABOUR");
        }
    }
}
