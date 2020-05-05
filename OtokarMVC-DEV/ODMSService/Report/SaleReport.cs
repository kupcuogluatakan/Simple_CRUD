using ODMSBusiness;
using ODMSData;
using ODMSModel.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Security;

namespace ODMSService.Report
{
    public class SaleReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Satış Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new SaleReportFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            int totalRowCount;
            return _reportData.GetSaleReport(UserInfo, Filter as SaleReportFilterRequest, out totalRowCount);
        }
    }
}
