using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.Reports;

namespace ODMSService.Report
{
    class GuaranteeReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Garanti Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new GuaranteeReportFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.GetGuaranteeReport(Filter as GuaranteeReportFilterRequest, out total).Data;
        }
    }
}
