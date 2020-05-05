using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.Price;
using ODMSModel.Reports;
using ODMSCommon.Security;

namespace ODMSService.Report
{
    class WorkOrderDetailReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "İş Emri Kartı Detay Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new WorkOrderDetailReportListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var totalCnt = 0;
            var totalVehicle = 0;
            return reportsBo.ListWorkOrderDetailReport(UserInfo, Filter as WorkOrderDetailReportListModel, out totalCnt, out totalVehicle).Data;
        }
    }
}
