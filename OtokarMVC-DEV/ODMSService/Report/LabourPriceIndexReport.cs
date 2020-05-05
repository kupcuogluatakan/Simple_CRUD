using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.LabourPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class LabourPriceIndexReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "İşcilik Fiyatı Tanımlamaİşcilik Fiyatı Tanımlama";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new LabourPriceListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var bus = new LabourPriceBL();
            int totalCnt = 0;
            return bus.ListLabourPrices(UserInfo, (Filter as LabourPriceListModel), out totalCnt).Data;
        }
    }
}
