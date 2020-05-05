using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class CustomerReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Müşteri Listesi";
            }
        }

        private object _filter;

        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new CustomerListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var customerBo = new CustomerBL();
            int totalCnt = 0;
            return customerBo.ListCustomers(UserInfo, (Filter as CustomerListModel), out totalCnt).Data;
        }
    }
}
