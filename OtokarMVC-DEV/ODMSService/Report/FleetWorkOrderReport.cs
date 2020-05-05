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
    class FleetWorkOrderReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Filo İş Emri Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new FleetPeriodReportFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.ListFleetPeriodReport(UserInfo, Filter as FleetPeriodReportFilterRequest, out total).Data;
        }
    }
}

