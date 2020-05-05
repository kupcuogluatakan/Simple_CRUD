
namespace ODMSBusiness.Reports
{
    internal class SparePartPositionedReport:ReportBase,IReport
    {

        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.SparePartPositionedReport(Credentials, Language, (int) parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.ReturnSparePartPositionedReportAsync(Credentials, Language, (int)parameters[0]).Result.ReturnSparePartPositionedReportResult;
        }
    }
}
