using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.WorkOrder;

namespace ODMSService.Report
{
    class WorkOrderReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "İş Emirleri";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new WorkOrderListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var bus = new WorkOrderBL();
            var totalCnt = 0;
            return bus.ListWorkOrders(UserInfo,Filter as WorkOrderListModel, out totalCnt).Data;
        }
    }
}