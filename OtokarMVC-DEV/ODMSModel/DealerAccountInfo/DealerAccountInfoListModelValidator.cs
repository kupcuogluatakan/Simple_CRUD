using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.DealerAccountInfo
{
    public class DealerAccountInfoListModelValidator : AbstractValidator<DealerAccountListModel>
    {
        public DealerAccountInfoListModelValidator()
        {
            RuleFor(c => c.BankId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Branch).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Iban).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);

        }
    }
}
