using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.Labour;

namespace ODMSService.Report
{
    class LabourDetailReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "İşçilik Süreleri ve Detayları";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new LabourListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new LabourBL();

            return reportsBo.GetLabourListForExcel(UserInfo).Data;
        }
    }
}