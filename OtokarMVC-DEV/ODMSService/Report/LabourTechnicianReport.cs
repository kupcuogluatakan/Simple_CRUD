using ODMSBusiness;
using ODMSBusiness.Reports;
using ODMSModel.LabourTechnician;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    public class LabourTechnicianReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "İşçilik Teknisyen Tanımlama";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new LabourTechnicianListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new LabourTechnicianBL();
            var totalCount = 0;

            return reportsBo.ListLabourTechnicians(UserInfo, Filter as LabourTechnicianListModel, out totalCount).Data;
        }
    }
}
