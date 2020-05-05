using ODMSBusiness;
using ODMSModel.StockCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Security;

namespace ODMSService.Report
{
    public class StockSearchReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Stok Arama";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new StockCardSearchListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            int totalRowCount;
            var stockTypeDetailBo = new StockCardBL();
            return stockTypeDetailBo.ListStockCardSearch(UserInfo, Filter as StockCardSearchListModel, out totalRowCount).Data;
        }
    }
}
