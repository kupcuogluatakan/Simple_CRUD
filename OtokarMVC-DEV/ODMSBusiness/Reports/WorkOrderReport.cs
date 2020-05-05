
namespace ODMSBusiness.Reports
{
    internal class WorkOrderReport:ReportBase,IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.WorkOrderReport(Credentials, (long) parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.WorkOrderReportAsync(Credentials, (int) parameters[0]).Result.WorkOrderReportResult;
        }
    }
}
