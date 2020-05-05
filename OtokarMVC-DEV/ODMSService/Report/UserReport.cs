using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class UserReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Kullanıcılar";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new UserListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var userBo = new UserBL();
            int totalCnt = 0;
            return userBo.ListUsers(UserInfo, (Filter as UserListModel), out totalCnt).Data;
        }
    }
}
