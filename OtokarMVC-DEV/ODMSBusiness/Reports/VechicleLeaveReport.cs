
namespace ODMSBusiness.Reports
{
    internal class VechicleLeaveReport:ReportBase,IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.VehicleDeliveryVoucherReport(Credentials, (long) parameters[0]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.VehicleDeliveryVoucherReportAsync(Credentials, (long) parameters[0]).Result.VehicleDeliveryVoucherReportResult;
        }

    }
}
