using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.DealerStartupInventoryLevel;

namespace ODMSService.Report
{
    class DealerStartupInventoryLevelReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Başlangıç Stok Seviyesi Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new DealerStartupInventoryLevelListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            DealerStartupInventoryLevelBL dealerStartupInventoryLevelBL = new DealerStartupInventoryLevelBL();
            int totalCount = 0;
            return dealerStartupInventoryLevelBL.ListDealerStartupInventoryLevels(UserInfo,(Filter as DealerStartupInventoryLevelListModel), out totalCount).Data;

        }
    }
}
