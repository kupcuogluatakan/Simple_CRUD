using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalCancelModel:ModelBase
    {
        public long ProposalId { get; set; }
        public byte ProposalSeq { get; set; }

        [Required(ErrorMessageResourceName = "WorkOrderCard_Display_CancelProposalMessage", ErrorMessageResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CancelReason { get; set; }
    }
}
