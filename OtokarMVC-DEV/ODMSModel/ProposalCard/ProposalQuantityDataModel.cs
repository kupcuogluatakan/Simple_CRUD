using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    [Validator(typeof(ProposalQuantityDataModelValidator))]
    public class ProposalQuantityDataModel : ModelBase
    {
        public long ProposalId { get; set; }
        public long ProposalDetailId { get; set; }
        public string Type { get; set; }
        public long ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Duration { get; set; }
        public bool LabourDealerDurationCheck { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
