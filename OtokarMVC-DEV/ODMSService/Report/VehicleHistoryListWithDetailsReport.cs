using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSModel.ListModel;

namespace ODMSService.Report
{
    class VehicleHistoryListWithDetailsReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Araç Geçmişi Detaylı Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new VehicleHistoryListWithDetailsModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var bus = new VehicleBL();
           
            var totalCnt = 0;
            return bus.ListVehicleHistoryListWithDetails(UserInfo,Filter as VehicleHistoryListWithDetailsModel, out totalCnt).Data;
        }
    }
}