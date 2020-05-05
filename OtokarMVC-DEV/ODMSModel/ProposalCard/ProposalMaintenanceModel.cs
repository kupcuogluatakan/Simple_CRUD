using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalMaintenanceModel : ModelBase
    {
        public long ProposalId { get; set; }
        public int ProposalSeq { get; set; }
        public long ProposalDetailId { get; set; }
        public int MaintenanceId { get; set; }
        public string MaintenancName { get; set; }
        public int MaintenanceKilometer { get; set; }
        public int VehicleKilometer { get; set; }
    }
}
