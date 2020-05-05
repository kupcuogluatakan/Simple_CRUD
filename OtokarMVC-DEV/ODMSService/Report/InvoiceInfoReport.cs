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
    class InvoiceInfoReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Fatura Bilgileri Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new InvoiceInfoFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.ListInvoiceInfoReport(UserInfo, Filter as InvoiceInfoFilterRequest, out total).Data;
        }
    }
}
