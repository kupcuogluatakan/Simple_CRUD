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
    class PartExchangeReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Araç ve Servis Bazlı Parça Değişim Raporu";
            }
        }

        private object _filter;

        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new PartExchangeFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.GetPartExchangeReport(UserInfo, Filter as PartExchangeFilterRequest, out total).Data;
        }
    }
}