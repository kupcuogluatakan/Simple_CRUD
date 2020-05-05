using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Reports
{
    internal class SparePartSaleWaybillCopyReport : ReportBase, IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.SparePartSaleWaybillCopyReport(Credentials, (long)parameters[0], (int)parameters[1], (string)parameters[2]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            return Client.SparePartSaleWaybillCopyReportAsync(Credentials, (long)parameters[0], (int)parameters[1], (string)parameters[2]).Result.SparePartSaleWaybillCopyReportResult;
        }
    }
}
