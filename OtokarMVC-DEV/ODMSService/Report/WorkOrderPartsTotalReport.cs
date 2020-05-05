using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.Reports;
using ODMSCommon.Security;

namespace ODMSService.Report
{
    class WorkOrderPartsTotalReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Parça Bazlı Tutarsal – Adetsel Rapor";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new WorkOrderPartsTotalReportFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.GetWorkOrderPartsTotalReport(UserInfo, Filter as WorkOrderPartsTotalReportFilterRequest, out total).Data;
        }
    }
}