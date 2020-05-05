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
    class WorkOrderMainReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Servis Periyodik Bakım Fatura Sorgulama Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new WorkOrderMaintReportListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var totalCnt = 0;
            var totalVehicle = 0;
            return reportsBo.ListWorkOrderMaintReport(UserInfo, Filter as WorkOrderMaintReportListModel, out totalCnt).Data;
        }
    }
}
