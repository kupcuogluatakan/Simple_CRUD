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
    class PartStockActivityReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Yedek Parça Hareketleri Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new PartStockActivityFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.GetPartStockActivityReport(Filter as PartStockActivityFilterRequest, out total).Data;
        }
    }
}
