namespace ODMSBusiness.Reports
{
    internal class ClaimDismantledPartRealReport:ReportBase,IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.ClaimDismantledPartReportReal(Credentials, Language, (int) parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.ClaimDismantledPartReportRealAsync(Credentials, Language, (int)parameters[0]).Result.ClaimDismantledPartReportRealResult;
        }
    }
}
