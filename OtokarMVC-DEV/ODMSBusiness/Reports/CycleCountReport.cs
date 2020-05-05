
namespace ODMSBusiness.Reports
{
    public class CycleCountReport : ReportBase, IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.CycleCountPlanReport(Credentials, Language, (int)parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.CycleCountPlanReportAsync(Credentials, Language, (int)parameters[0]).Result.CycleCountPlanReportResult;
        }
    }
}
