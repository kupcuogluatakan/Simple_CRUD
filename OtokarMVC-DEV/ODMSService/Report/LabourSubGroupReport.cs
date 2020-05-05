using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.LabourSubGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class LabourSubGroupReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "İşçilik Alt Grup Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new LabourSubGroupListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            LabourSubGroupBL _service = new LabourSubGroupBL();
            int totalCnt = 0;
            return _service.List(UserInfo, (Filter as LabourSubGroupListModel), out totalCnt).Data;
        }
    }
}
