using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Contract
{
    public class ContractViewModelValidator : AbstractValidator<ContractViewModel>
    {
        public ContractViewModelValidator()
        {
            RuleFor(c => c.ContractName).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StartDate).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.EndDate).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Duration).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.DocId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ContractName)
                .Length(0, 250)
                .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.EndDate)
            .GreaterThan(o => o.StartDate.Value)
            .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                               MessageResource.Campaign_Display_StartDate,
                               MessageResource.Campaign_Display_EndDate));
        }
    }
}
