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
    class WorkOrderProcessTypesTotalReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "İş Emri Kartı Adetsel – Tutarsal Dağılım Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new WorkOrderProcessTypesFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            return reportsBo.GetWorkOrderProcessTypesTotalReport(UserInfo, Filter as WorkOrderProcessTypesFilterRequest).Items;
        }
    }
}
