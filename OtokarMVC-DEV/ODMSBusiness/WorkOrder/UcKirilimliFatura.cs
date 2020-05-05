
namespace ODMSBusiness.WorkOrder
{
    internal class UcKirilimliFatura:InvoiceBase
    {

        public UcKirilimliFatura(long invoiceId):base(invoiceId)
        {
        }
        public  byte[] Print(InvoicePrintType printType)
        {
            byte[] result;
            switch (printType)
            {
                case InvoicePrintType.Printed:
                    result=Client.WorkOrderInvioceBreakDownReportReal(Credentials, Language, InvoiceId);
                    break;
                case InvoicePrintType.Transcript:
                    result = Client.WorkOrderInvioceBreakDownReportCopy(Credentials, Language, InvoiceId);
                    break;
                case InvoicePrintType.Proforma:
                case InvoicePrintType.WorkOrderAndProforma:
                    result = Client.WorkOrderInvioceBreakDownProformaReport(Credentials, Language, InvoiceId);
                    break;
                case InvoicePrintType.ProformaExcel:
                    result = Client.WorkOrderInvioceBreakDownProformaExcelReport(Credentials, Language, InvoiceId);
                    break;
                default:
                    result=null;
                    break;
            }
            return result;
        }

        public override byte[] FetchBytes(params object[] parameters)
        {
            return Print((InvoicePrintType) parameters[0]);
        }

        public override byte[] FetchBytesAsync(params object[] parameters)
        {
            return Print((InvoicePrintType) parameters[0]);
        }
    }
}
