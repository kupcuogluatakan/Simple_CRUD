using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.Reports;
using ODMSCommon.Security;

namespace ODMSService.Report
{
    class LaborCostPerVehicleReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Araç Başı İşcilik Gideri Raporu";
            }
        }


        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new LaborCostPerVehicleReportFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.GetLaborCostPerVehicleReport(UserInfo, Filter as LaborCostPerVehicleReportFilterRequest, out total).Data;
        }
    }
}