using ODMSBusiness.WorkOrder;

namespace ODMSBusiness.Reports
{
    public class VehicleInvoiceReport:ReportBase,IReport
    {
        
        public byte[] FetchBytes(params object[] parameters)
        {
            var invoice = InvoiceFactory.Create((InvoiceType) parameters[0], (long) parameters[1], (bool) parameters[2]);
            return invoice.FetchBytes((InvoicePrintType) parameters[3]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            var invoice = InvoiceFactory.Create((InvoiceType)parameters[0], (long)parameters[1], (bool)parameters[2]);
            return invoice.FetchBytesAsync((InvoicePrintType)parameters[3]);
        }
    }
}
