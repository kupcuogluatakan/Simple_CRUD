using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.AppointmentIndicatorCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class AppointmentIndicatorCategoryReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Belirti Kodu Alt Grup Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new AppointmentIndicatorCategoryListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            AppointmentIndicatorCategoryBL appointmentIndicatorCategoryBL = new AppointmentIndicatorCategoryBL();
            int totalCount = 0;
            return appointmentIndicatorCategoryBL.GetAppointmentIndicatorCategoryList(UserInfo,(Filter as AppointmentIndicatorCategoryListModel), out totalCount).Data;
            
        }
    }
}
