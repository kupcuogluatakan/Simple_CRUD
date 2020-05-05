using ODMSBusiness;
using ODMSModel.Rack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class RackReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Raf Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new RackListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var bo = new RackBL();
            int totalCnt = 0;
            return bo.ListRacks((Filter as RackListModel), out totalCnt).Data;
        }
    }
}
