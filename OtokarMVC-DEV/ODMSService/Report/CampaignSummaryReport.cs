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
    class CampaignSummaryReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Kampanya Özet Raporu";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new CampaignSummaryReportFilterRequest();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ReportsBL();
            var total = 0;
            return reportsBo.GetCampaignSummaryReport(UserInfo, Filter as CampaignSummaryReportFilterRequest, out total).Data;
        }
    }
}
