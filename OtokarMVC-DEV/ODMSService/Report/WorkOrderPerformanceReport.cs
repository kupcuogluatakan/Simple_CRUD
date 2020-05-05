using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.Reports;

namespace ODMSService.Report
{
    class WorkOrderPerformanceReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "İş Emri Performans Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new WorkOrderPerformanceReportFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.GetWorkOrderPreformanceReport(Filter as WorkOrderPerformanceReportFilterRequest, out total).Data;
        }
    }
}
