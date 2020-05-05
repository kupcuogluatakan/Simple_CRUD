using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class GeneralInfo
    {
        public long ProposalId { get; set; }
        public int ProposalSeq { get; set; }
        public string Matter1 { get; set; }
        public string Matter2 { get; set; }
        public string Matter3 { get; set; }
        public string Matter4 { get; set; }
        public string[] TechnicalDesc { get; set; }
        //WitholdingStatus
        public int? WitholdingStatus { get; set; }
        public string WitholdingStatusName { get; set; }
        //WitholdingId
        public string WitholdingId { get; set; }
        public string WitholdingName { get; set; }
    }
}
