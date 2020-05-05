using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.StockTypeDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class StockTypeDetailReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Bayi Stok Arama";
            }
        }

        private object _filter;

        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new StockTypeDetailListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var stockTypeDetailBo = new StockTypeDetailBL();
            int totalCnt = 0;
            return stockTypeDetailBo.ListStockTypeDetail(UserInfo, (Filter as StockTypeDetailListModel), out totalCnt).Data;
        }
    }
}
