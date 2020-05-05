
namespace ODMSBusiness.WorkOrder
{
    internal class DokumluFatura:InvoiceBase
    {
        private bool hasWitholding;

        public DokumluFatura(long invoiceId,bool hasWitholding):base(invoiceId)
        {
            this.hasWitholding = hasWitholding;
        }

        private  byte[] FetchBytesAsyncInternal(InvoicePrintType printType)
        {
            byte[] result;
            switch (printType)
            {
                case InvoicePrintType.Printed:
                    if (hasWitholding)
                    {
                        result= Client.WorkOrderInvoiceWithHoldReportReal(Credentials, Language, InvoiceId);
                        
                    }
                    else
                    {
                        result = Client.WorkOrderInvoiceReportReal(Credentials, Language, InvoiceId);
                    }
                    break;
                case InvoicePrintType.Transcript:
                    if (hasWitholding)
                    {
                        result= Client.WorkOrderInvoiceWithHoldReportCopy(Credentials, Language, InvoiceId);
                    }
                    else
                    {
                        result= Client.WorkOrderInvoiceReportCopy(Credentials, Language, InvoiceId);
                    }
                    break;
                case InvoicePrintType.WorkOrderAndProforma:
                case InvoicePrintType.Proforma:
                    if (hasWitholding)
                    {
                        result = Client.WorkOrderInvoiceWithHoldProformaReport(Credentials, Language, InvoiceId);
                    }
                    else
                    {
                        result = Client.WorkOrderInvoiceProformaReport(Credentials, Language, InvoiceId);
                    }
                    break;
                case InvoicePrintType.ProformaExcel:
                    if (hasWitholding)
                    {
                        result = Client.WorkOrderInvoiceWithHoldProformaExcelReport(Credentials, Language, InvoiceId);
                    }
                    else
                    {
                        result = Client.WorkOrderInvoiceProformaExcelReport(Credentials, Language, InvoiceId);
                    }
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }

        public override byte[] FetchBytes(params object[] parameters)
        {
            return FetchBytesAsyncInternal((InvoicePrintType)parameters[0]);
        }

        public  override byte[] FetchBytesAsync(params object[] parameters)
        {
            return FetchBytesAsyncInternal((InvoicePrintType)parameters[0]);
        }
    }
}
