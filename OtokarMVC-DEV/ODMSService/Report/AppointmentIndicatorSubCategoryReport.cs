using ODMSBusiness;
using ODMSModel.AppointmentIndicatorSubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class AppointmentIndicatorSubCategoryReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Belirti Kodu Son Grup Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new AppointmentIndicatorSubCategoryListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            AppointmentIndicatorSubCategoryBL appointmentIndicatorSubCategoryBL = new AppointmentIndicatorSubCategoryBL();
            int totalCount = 0;
            return appointmentIndicatorSubCategoryBL.GetAppointmentIndicatorSubCategoryList(UserInfo,(Filter as AppointmentIndicatorSubCategoryListModel), out totalCount).Data;

        }
    }
}
