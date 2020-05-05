namespace ODMSModel.WorkOrderInvoice
{
    public class WorkOrderInvoiceDTO
    {
        public int WorkOrderId { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal InvoiceAmountWithVat { get; set; }
        public string CurrencyCode { get; set; }
        public decimal InvoiceRatio { get; set; }
        public decimal VatRatio { get; set; }
        public bool HasWitholding { get; set; }
        public bool IsFromProposalWitholding { get; set; }
        public int WitholdId { get; set; }
        public decimal WitholdAmount { get; set; }
        public decimal WitholdingMinAmount { get; set; }
        public int WitholdDivident { get; set; }
        public int WitholdDivisor { get; set; }
        public int ErrorNo { get; set; }
        public string ErrorDesc { get; set; }
        public long WorkOrderInvoiceId { get; set; }
    }
}
