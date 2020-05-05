using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.CycleCountStockDiff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class CycleCountStockDiffSearchReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Orjinal Parça Adet Değişim Sorgulama";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new CycleCountStockDiffSearchListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var bo = new CycleCountStockDiffBL();
            int totalCnt = 0;
            return bo.SearchCycleCountStockDiffs(UserInfo, (Filter as CycleCountStockDiffSearchListModel), out totalCnt).Data;
        }
    }
}
