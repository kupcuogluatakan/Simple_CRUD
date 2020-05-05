
namespace ODMSBusiness.Reports
{
    internal class SparePartPickingReport : ReportBase, IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.SparePartPickingReport(Credentials, Language, (int)parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.SparePartPickingReportAsync(Credentials, Language, (int)parameters[0]).Result.SparePartPickingReportResult;
        }
    }
}
