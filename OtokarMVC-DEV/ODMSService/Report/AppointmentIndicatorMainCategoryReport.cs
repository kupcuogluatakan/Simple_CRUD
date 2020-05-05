using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.AppointmentIndicatorMainCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class AppointmentIndicatorMainCategoryReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Belirti Ana Grup Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new AppointmentIndicatorMainCategoryListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            AppointmentIndicatorMainCategoryBL appointmentIndicatorMainCategoryBL = new AppointmentIndicatorMainCategoryBL();
            int totalCount = 0;
            return appointmentIndicatorMainCategoryBL.GetAppointmentIndicatorMainCategoryList(UserInfo,(Filter as AppointmentIndicatorMainCategoryListModel), out totalCount).Data;
        }
    }
}
