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
    class PurchaseOrderReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Satın Alma Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new PurchaseOrderFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            decimal totalPrice = 0;
            var total = 0;
            return reportsBo.GetPurchaseOrderReport(UserInfo, Filter as PurchaseOrderFilterRequest, out totalPrice, out total).Data;
        }
    }
}
