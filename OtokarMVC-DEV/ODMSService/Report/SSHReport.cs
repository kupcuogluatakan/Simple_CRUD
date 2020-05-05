using ODMSBusiness;
using ODMSModel.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class SSHReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "SSH Bilgi Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new SshReportFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.ListSshReport(UserInfo, Filter as SshReportFilterRequest, out total).Data;
        }
    }
}
