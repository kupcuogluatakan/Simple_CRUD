using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.StockCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class OtokarStockSearchReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Otokar Stok Sorgulama";
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
            StockCardBL bo = new StockCardBL();
            int totalCnt = 0;
            return bo.ListStockCardSearch(UserInfo, (Filter as StockCardSearchListModel), out totalCnt).Data;
        }
    }
}
