using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Business;
using ODMSModel.ListModel;

namespace ODMSService.Report
{
    class VehicleHistoryReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Araç Geçmişi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new VehicleHistoryListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var bus = new VehicleHistoryBL();
            var totalCnt = 0;
            return bus.ListVehicleHistory(UserInfo,Filter as VehicleHistoryListModel, out totalCnt).Data;
        }
    }
}