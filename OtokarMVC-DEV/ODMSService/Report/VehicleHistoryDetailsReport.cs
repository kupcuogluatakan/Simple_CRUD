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
    class VehicleHistoryDetailsReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Araç Geçmişi Detayları";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new VehicleHistoryDetailListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var bus = new VehicleBL();
           
            var totalCnt = 0;
            return bus.ListVehicleHistoryAllDetails(UserInfo,Filter as VehicleHistoryDetailListModel, out totalCnt).Data;
        }
    }
}