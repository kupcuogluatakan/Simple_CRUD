
namespace ODMSBusiness.Reports
{
    public class ClaimDismantledPartProformaReport : ReportBase, IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.ClaimDismantledPartProformaReport(Credentials, Language, (int)parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.ClaimDismantledPartProformaReportAsync(Credentials, Language, (int)parameters[0]).Result.ClaimDismantledPartProformaReportResult;
        }
    }
}
