using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class MaintenanceReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Araç Bakım Paketleri";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new MaintenanceListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            MaintenanceBL maintBL = new MaintenanceBL();
            int totalCount = 0;
            return maintBL.GetMaintenanceList(UserInfo, (Filter as MaintenanceListModel), out totalCount).Data;
        }
    }
}
