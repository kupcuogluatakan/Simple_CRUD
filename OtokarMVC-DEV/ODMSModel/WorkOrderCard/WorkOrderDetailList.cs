
namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderDetailList
    {
        public long WorkOrderDetailId { get; set; }
        public long? GifNo { get; set; }
        public string CancelReason { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsBillable { get; set; }
        public bool InvoiceCancel { get; set; }
        public DetailType DetailType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string FailureCode { get; set; }
        public string FailureCodeDescription { get; set; }
        public string ProcessType { get; set; }
        public string ProcessTypeCode { get; set; }
        public bool GuarantyAuthorizationNeed{ get; set; }
        public int WarrantyStatus{ get; set; }
        public string WarrantyStatusDesc { get; set; }
        public long? InvoiceId { get; set; }
        public bool GosApprovalNeed { get; set; }
        public bool IsFromProposal { get; set; }
        public string GuaranteeConfirmDesc { get; set; }

        public string TechicalDescription { get; set; }
        public bool GosSendCheck { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public bool AllowFailureCodeChange { get; set; }
        public bool IsCampaignCritical { get; set; }
    }

    public enum DetailType
    {
        Part,
        Labour,
        Meintenance,
        Indicator
    }

    public enum IndicatorType:short
    {
        BreakDown=0,
        Campaign,
        CouponMaint,
        PeriodicMaint,
        Pdi,
        HourMaint
    }
}
