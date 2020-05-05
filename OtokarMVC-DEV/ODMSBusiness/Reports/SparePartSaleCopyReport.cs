
namespace ODMSBusiness.Reports
{
    internal class SparePartSaleCopyReport:ReportBase,IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.SparePartSaleReportCopy(Credentials, Language, (int) parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.SparePartSaleReportCopyAsync(Credentials, Language, (int)parameters[0]).Result.SparePartSaleReportCopyResult;
        }
    }
}
