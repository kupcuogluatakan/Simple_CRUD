using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.Labour;

namespace ODMSService.Report
{
    class LabourReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "İşçilik Süreleri";
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
            var totalCount = 0;

            return reportsBo.GetLabourList(UserInfo,Filter as LabourListModel, out totalCount).Data;
        }
    }
}
