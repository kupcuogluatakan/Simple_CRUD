using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ContractPart
{
    public class ContractPartViewModelValidator : AbstractValidator<ContractPartViewModel>
    {
        public ContractPartViewModelValidator()
        {
            RuleFor(c => c.IdContract).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdPart).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
