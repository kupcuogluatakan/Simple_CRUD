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
    class ChargePerCarReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Araç Bazlı Gider Hesaplama Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new ChargePerCarFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.GetChargePerCarReport(UserInfo, Filter as ChargePerCarFilterRequest, out total).Data;
        }
    }
}
