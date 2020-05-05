
namespace ODMSBusiness.Reports
{
    internal class SparePartSaleRealReport:ReportBase,IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.SparePartSaleReportReal(Credentials, Language, int.Parse(parameters[0].ToString()));
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.SparePartSaleReportRealAsync(Credentials, Language, (int)parameters[0]).Result.SparePartSaleReportRealResult;
        }
    }
}
