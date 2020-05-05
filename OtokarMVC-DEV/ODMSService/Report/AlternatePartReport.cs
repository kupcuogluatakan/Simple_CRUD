using ODMSBusiness;
using ODMSModel.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class AlternatePartReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Alternatif Parça Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new AlternatePartReportFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.GetAlternatePartReport(UserInfo, Filter as AlternatePartReportFilterRequest, out total).Data;
        }
    }
}
