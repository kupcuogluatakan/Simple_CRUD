using System.Threading.Tasks;

namespace ODMSBusiness.WorkOrder
{
    internal class OzelFatura:InvoiceBase
    {
        public OzelFatura(long invoiceId):base(invoiceId)
        {
        }
        public async Task<byte[]> Print(InvoicePrintType printType)
        {
            byte[] result;
            switch (printType)
            {
                case InvoicePrintType.Printed:
                    var responseReal = await Client.SpecialWorkOrderInvoiceReportRealAsync(Credentials, Language, InvoiceId);
                    result = responseReal.SpecialWorkOrderInvoiceReportRealResult;
                    break;
                case InvoicePrintType.Transcript:
                    var responseCopy = await Client.SpecialWorkOrderInvoiceReportCopyAsync(Credentials, Language, InvoiceId);
                    result = responseCopy.SpecialWorkOrderInvoiceReportCopyResult;
                    break;
                case InvoicePrintType.Proforma:
                    var responseProforma = await Client.SpecialWorkOrderInvoiceProformaReportAsync(Credentials, Language, InvoiceId);
                    result = responseProforma.SpecialWorkOrderInvoiceProformaReportResult;
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }

        public override byte[] FetchBytes(params object[] parameters)
        {
            var printType = (InvoicePrintType) parameters[0];
            byte[] result;
            switch (printType)
            {
                case InvoicePrintType.Printed:
                    result = Client.SpecialWorkOrderInvoiceReportReal(Credentials, Language, InvoiceId);
                    break;
                case InvoicePrintType.Transcript:
                    result =  Client.SpecialWorkOrderInvoiceReportCopy(Credentials, Language, InvoiceId);
                    break;
                case InvoicePrintType.Proforma:
                    result = Client.SpecialWorkOrderInvoiceProformaReport(Credentials, Language, InvoiceId);
                    break;
                case InvoicePrintType.ProformaExcel:
                    result = Client.SpecialWorkOrderInvoiceProformaExcelReport(Credentials, Language, InvoiceId);
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }

        public override  byte[] FetchBytesAsync(params object[] parameters)
        {
            return  Print((InvoicePrintType)parameters[0]).Result;
        }
    }
}
