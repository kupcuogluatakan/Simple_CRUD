using ODMSModel.WorkOrderCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    public class ProposalDetailList
    {
        public long ProposalDetailId { get; set; }
        public long? GifNo { get; set; }
        public string CancelReason { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsBillable { get; set; }
        public DetailType DetailType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string FailureCode { get; set; }
        public string FailureCodeDescription { get; set; }
        public string ProcessType { get; set; }
        public string ProcessTypeCode { get; set; }
        public bool GuarantyAuthorizationNeed { get; set; }
        public int WarrantyStatus { get; set; }
        public string WarrantyStatusDesc { get; set; }
        public bool InvoiceCancel { get; set; }
        public long? InvoiceId { get; set; }
        public bool GosApprovalNeed { get; set; }
        public string GuaranteeConfirmDesc { get; set; }

        public string TechicalDescription { get; set; }
        public bool GosSendCheck { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public bool AllowFailureCodeChange { get; set; }
    }
}
