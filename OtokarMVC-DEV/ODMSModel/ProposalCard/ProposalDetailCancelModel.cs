using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    [Validator(typeof(ProposalDetailCancelModelValidator))]
    public class ProposalDetailCancelModel : ModelBase
    {
        public long ProposalId { get; set; }
        public long ProposalDetailId { get; set; }
        public string CancelReason { get; set; }
        public int ProposalSeq { get; set; }
    }
}
