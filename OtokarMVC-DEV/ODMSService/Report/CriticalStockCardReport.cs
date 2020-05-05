using ODMSBusiness;
using ODMSModel.CriticalStockCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class CriticalStockCardReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Kritik Stok Seviyesi Tanımlama";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new CriticalStockCardListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var criticalStockCardBo = new CriticalStockCardBL();
            int totalCnt = 0;
            return criticalStockCardBo.ListCriticalStockCard(UserInfo,(Filter as CriticalStockCardListModel), out totalCnt).Data;
        }
    }
}
