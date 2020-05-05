using ODMSBusiness.Reports;

namespace ODMSBusiness.WorkOrder
{
    internal abstract class InvoiceBase:ReportBase,IReport
    {

        protected long InvoiceId;

        public InvoiceBase(long invoiceId)
        {
            InvoiceId = invoiceId;
        }

        public abstract byte[] FetchBytes(params object[] parameters);

        public abstract byte[] FetchBytesAsync(params object[] parameters);
    }
}
