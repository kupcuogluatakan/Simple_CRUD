
namespace ODMSBusiness.Reports
{
    internal class ClaimDismantledPartCopyReport:ReportBase,IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.ClaimDismantledPartReportCopy(Credentials, Language, (int) parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.ClaimDismantledPartReportCopyAsync(Credentials, Language, (int)parameters[0]).Result.ClaimDismantledPartReportCopyResult;
        }
    }
}
