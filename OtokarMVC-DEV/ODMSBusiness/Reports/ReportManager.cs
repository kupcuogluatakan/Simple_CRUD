
namespace ODMSBusiness.Reports
{
    public static class ReportManager 
    {
        private static readonly object shared = new object();
        public static byte[] GetReport(ReportType type, params object[] parameters)
        {
            lock (shared)
            {
                return ReportFactory.Create(type).FetchBytes(parameters);
            }
        }
    }
}
