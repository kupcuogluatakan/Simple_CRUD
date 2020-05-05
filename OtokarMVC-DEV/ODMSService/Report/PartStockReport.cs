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
    class PartStockReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Yedek Parça Stok Sorgulama Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new PartStockFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            decimal totalPrice = 0;
            var total = 0;
            return reportsBo.GetPartStockReport(UserInfo, Filter as PartStockFilterRequest, out totalPrice, out total).Data;
        }
    }
}

