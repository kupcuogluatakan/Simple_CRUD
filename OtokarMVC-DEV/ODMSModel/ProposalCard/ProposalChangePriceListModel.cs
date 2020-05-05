using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    [Validator(typeof(ProposalChangePriceListModelValidator))]
    public class ProposalChangePriceListModel : ModelBase
    {
        public long ProposalId { get; set; }
        public long ProposalDetailId { get; set; }
        public string Type { get; set; }
        public long ItemId { get; set; }
        public DateTime ProposalDate { get; set; }
        public DateTime PriceListDate { get; set; }
    }
}
