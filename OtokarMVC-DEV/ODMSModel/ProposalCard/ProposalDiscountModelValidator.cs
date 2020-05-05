using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalDiscountModelValidator : AbstractValidator<ProposalDiscountModel>
    {
        public ProposalDiscountModelValidator()
        {
            RuleFor(c => c.ProposalId).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.ItemId).NotEmpty();
            RuleFor(c => c.ProposalDetailId).NotEmpty();
            RuleFor(c => c.DiscountPrice).GreaterThanOrEqualTo(0).When(c => c.DiscountType == DiscountType.Money);
            RuleFor(c => c.DiscountRatio).GreaterThanOrEqualTo(0).When(c => c.DiscountType == DiscountType.Percentage);
            RuleFor(c => c.DiscountRatio).LessThanOrEqualTo(100).When(c => c.DiscountType == DiscountType.Percentage);
            RuleFor(c => c.DiscountPrice).Must(c => c.ToString().Length < 11);
            RuleFor(c => c.DiscountRatio).Must(c => c.ToString().Length < 6);
            RuleFor(c => c.Type).Must(c => c == "PART" || c == "LABOUR");
        }
    }
}
