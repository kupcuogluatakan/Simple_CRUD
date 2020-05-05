using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.Price;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class PriceReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Fiyat Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new PriceListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var bl = new PriceBL();
            int totalCount = 0;
            return bl.ListPrice(UserInfo, (Filter as PriceListModel), out totalCount).Data;
        }
    }
}
