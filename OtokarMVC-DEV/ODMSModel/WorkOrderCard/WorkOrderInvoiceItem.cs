namespace ODMSModel.WorkOrderCard
{
    public class WorkOrderInvoiceItem
    {
        public long WorkOrderDetailId { get; set; }
        public string Indicator { get; set; }
        public string PriceString { get; set; }
        public decimal Price { get; set; }
        public long WorkOrderInvoiceId { get; set; }
        public bool InvoiceCancel { get; set; }
        public long WorkOrderId { get; set; }
    }
}
