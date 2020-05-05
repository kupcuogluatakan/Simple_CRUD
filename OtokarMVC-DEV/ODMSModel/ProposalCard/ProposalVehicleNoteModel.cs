using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalVehicleNoteModel : ModelBase
    {
        public long ProposalId { get; set; }
        public int ProposalSeq { get; set; }
        public string Note { get; set; }
    }
}
