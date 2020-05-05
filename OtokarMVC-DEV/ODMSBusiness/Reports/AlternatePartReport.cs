using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Reports
{
    public class AlternatePartReport : ReportBase, IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.Proposal(Credentials, (long)parameters[0], (int)parameters[1]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
