
namespace ODMSBusiness.Reports
{
    internal class ReturnSparePartPickingReport : ReportBase ,IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.ReturnSparePartPositionedReport(Credentials, Language, (int)parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.ReturnSparePartPositionedReportAsync(Credentials, Language, (int)parameters[0]).Result.ReturnSparePartPositionedReportResult;
        }
    }
}
