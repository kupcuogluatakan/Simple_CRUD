namespace ODMSBusiness.Reports
{
    public class SparePartSaleProformaReport : ReportBase, IReport
    {
        //public byte[] FetchBytes(params object[] parameters)
        //{
        //    return Client.SparePartSaleProformaReport(Credentials, Language, (int)parameters[0]);
        //}

        //public byte[] FetchBytesAsync(params object[] parameters)
        //{
        //    return Client.SparePartSaleProformaReportAsync(Credentials, Language, (int)parameters[0]).Result.SparePartSaleProformaReportResult;
        //}

        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.SparePartSaleProformaReport(Credentials, Language, int.Parse(parameters[0].ToString()));
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.SparePartSaleProformaReportAsync(Credentials, Language, (int)parameters[0]).Result.SparePartSaleProformaReportResult;
        }
    }
}
