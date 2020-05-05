using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalCampaignRequestViewModelValidator : AbstractValidator<ProposalCampaignRequestViewModel>
    {
        public ProposalCampaignRequestViewModelValidator()
        {
            RuleFor(v => v.RequestNote).Length(0, 200).WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.CampaignCode).Length(0, 8).WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.CampaignCode).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdDealer).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Quantity).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
